using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ActuallyStandard.Constants;
using ActuallyStandard.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Standard.Models;

namespace Standard.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _env;
        private IStringLocalizer<SharedResources> _localizer;
        private IConfiguration _configuration;
        private IStringLocalizerFactory _stringLocalizerFactory;

        public HomeController(IConfiguration configuration,
                              IHostingEnvironment env,
                              IStringLocalizerFactory stringLocalizerFactory,
                              IStringLocalizer<SharedResources> localizer)
        {
            _env = env;
            _localizer = localizer;
            _configuration = configuration;
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        public IActionResult Index()
        {
            var context = HttpContext;
            var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;
            HttpContext.Items["culture"] = requestCulture.Culture;

            var location = Path.Combine(_env.ContentRootPath, "Resources");
            HttpContext.Items["location"] = location;
            var local = _stringLocalizerFactory.Create("SharedResources", location);
            HttpContext.Items["cookie_value"] = local["hello"];
            HttpContext.Items["resources"] = _localizer["hello"];


            var languageSwitch = context.Request.Query["lang"];

            var defaultCookieName = _configuration.GetValue<string>(Config.Localization_DefaultCookieName);
            if (languageSwitch == "fr")
            {
                var val = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fr-CA"));
                
                context.Response.Cookies.Append(defaultCookieName, val, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                context.Response.Redirect("/");
            }
            else if (languageSwitch == "en")
            {
                var val = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-CA"));
                context.Response.Cookies.Append(defaultCookieName, val, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                context.Response.Redirect("/");
            }

            return View();
        }

        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
