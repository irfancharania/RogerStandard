using System;
using System.Globalization;
using ActuallyStandard.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace ActuallyStandard.Helpers
{
    public static class LocalizationHelper
    {
        public static CultureInfo GetCultureInfo(string lang) {
            var cultureInfo = new CultureInfo(lang);
            cultureInfo.DateTimeFormat.ShortDatePattern = Config.ShortDatePattern;

            return cultureInfo;
        }

        public static void SetCultureCookie(HttpContext context
                                            , string cookieName
                                            , string culture)
        {
            var requestCulture = new RequestCulture(culture);
            
            context.Response.Cookies.Append(
                cookieName
                , CookieRequestCultureProvider.MakeCookieValue(requestCulture)
                , new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
        }
    }
}
