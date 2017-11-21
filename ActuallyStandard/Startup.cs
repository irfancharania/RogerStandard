using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ActuallyStandard.Constants;
using ActuallyStandard.Localization;
using ActuallyStandard.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Standard.Services;

namespace Standard
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddRouting(
                    options =>
                    {
                        options.AppendTrailingSlash = true;
                        options.LowercaseUrls = true;
                    })
                .AddJsonLocalization(options => options.ResourcesPath = "Resources")
                //.AddLocalization(options => options.ResourcesPath = "Resources")
                .Configure<RequestLocalizationOptions>(options =>
                    {
                        var supportedCultures = new List<CultureInfo> {
                                                        new CultureInfo("en-CA"),
                                                        new CultureInfo("fr-CA")
                        };
                        options.DefaultRequestCulture = new RequestCulture("en-CA");
                        options.SupportedCultures = supportedCultures;
                        options.SupportedUICultures = supportedCultures;
                        options.RequestCultureProviders
                                .OfType<CookieRequestCultureProvider>()
                                .First()
                                .CookieName = Configuration.GetValue<string>(AppSettings.Localization_DefaultCookieName);
                    }
                )
                .AddResponseCaching()
                .AddSingleton(Configuration)
                .AddTransient<IChangelogData, MockChangelogData>()
                .AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        return factory.Create(typeof(SharedResources));
                    };
                }
                )
            ;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
                            , IHostingEnvironment env
                            , ILoggerFactory loggerFactory
                            , IStringLocalizerFactory stringLocalizerFactory
                            , IConfiguration configuration
                            )
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseResponseCaching();
                app.UseExceptionHandler("/error/500");
            }

            //app.UseMiddleware<PersistLocalizationQueryString>(app, configuration);
            app.UsePersistLocalizationQueryString(configuration);

            app.UseStatusCodePages();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();
                    //headers.CacheControl = new CacheControlHeaderValue()
                    //{
                    //    MaxAge = TimeSpan.FromDays(365)
                    //};
                }
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id:int?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
