using System.Globalization;
using System.Threading.Tasks;
using ActuallyStandard.Constants;
using ActuallyStandard.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ActuallyStandard.Middleware
{
    public class PersistLocalizationQueryString
    {
        private readonly RequestDelegate _next;
        private readonly IApplicationBuilder _app;
        private readonly IConfiguration _configuration;

        public PersistLocalizationQueryString(RequestDelegate next
                                    , IApplicationBuilder app
                                    , IConfiguration configuration)
        {
            _next = next;
            _app = app;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var lang = context.Request.Query[Config.LocalizationDefaultQueryStringParameter];
            var locOptions = _app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            var culture = new CultureInfo(lang);
            if (locOptions.Value.SupportedCultures.Contains(culture))
            {
                var defaultCookieName = _configuration.GetValue<string>(AppSettings.LocalizationDefaultCookieName);
                LocalizationHelper.SetCultureCookie(context, defaultCookieName, lang);
            }

            await _next(context);
        }
    }

    public static class PersistLocalizationQueryStringExtensions
    {
        public static IApplicationBuilder UsePersistLocalizationQueryString(
            this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseWhen(
                (context) =>
                {
                    var path = context.Request.Path.Value;
                    var validPath = !(path.Contains("/api/") || path.Contains("/feed"));
                    var hasLangParameter = !string.IsNullOrWhiteSpace(context.Request.Query[Config.LocalizationDefaultQueryStringParameter]);

                    return validPath && hasLangParameter;
                },
                (builder) => builder.UseMiddleware<PersistLocalizationQueryString>(app, configuration)
                );

            return app;
        }
    }
}
