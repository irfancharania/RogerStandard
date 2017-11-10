using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Standard.Services;

namespace Standard
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json")
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
                .AddResponseCaching()
                .AddSingleton(Configuration)
                .AddTransient<IChangelogData, MockChangelogData>()
                .AddMvc()
            ;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
                            , IHostingEnvironment env
                            , ILoggerFactory loggerFactory
                            )
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseResponseCaching();
                app.UseExceptionHandler("/Error");
            }

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


            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}"
                );

            routeBuilder.MapRoute("Admin",
                "admin/{controller}/{action}/{id?}"
                );
        }
    }
}
