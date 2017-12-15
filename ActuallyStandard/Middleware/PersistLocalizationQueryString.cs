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
            var lang = context.Request.Query[Config.Localization_DefaultQueryStringParameter];
            var locOptions = _app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            var culture = new CultureInfo(lang);
            if (locOptions.Value.SupportedCultures.Contains(culture))
            {
                var defaultCookieName = _configuration.GetValue<string>(AppSettings.Localization_DefaultCookieName);
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
                    var isNotApi = !context.Request.Path.Value.Contains("/api/");
                    var hasLangParameter = !string.IsNullOrWhiteSpace(context.Request.Query[Config.Localization_DefaultQueryStringParameter]);

                    return isNotApi && hasLangParameter;
                },
                (builder) => builder.UseMiddleware<PersistLocalizationQueryString>(app, configuration)
                );

            return app;
        }
    }
}
