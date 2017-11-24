using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ActuallyStandard.Constants;
using ActuallyStandard.Helpers;
using ActuallyStandard.Localization;
using ActuallyStandard.Middleware;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ActuallyStandard.Services;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;

namespace ActuallyStandard
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
        public void ConfigureServices(IServiceCollection services)
        {
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
                                                        LocalizationHelper.GetCultureInfo("en-CA"),
                                                        LocalizationHelper.GetCultureInfo("fr-CA")
                        };
                        options.DefaultRequestCulture = new RequestCulture(supportedCultures.First());
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
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                }
                )
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
            services.AddAutoMapper(typeof(SharedResources));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
                            , IHostingEnvironment env
                            , ILoggerFactory loggerFactory
                            , IConfiguration configuration
                            )
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });

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
