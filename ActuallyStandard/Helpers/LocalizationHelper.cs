using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace ActuallyStandard.Helpers
{
    public class LocalizationHelper
    {
        public static void SetCultureCookie(HttpContext context
                                            , string cookieName
                                            , string culture) => 
            context.Response.Cookies.Append(
                cookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
    }
}
