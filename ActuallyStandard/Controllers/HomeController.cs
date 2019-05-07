using System.Diagnostics;
using ActuallyStandard.Constants;
using ActuallyStandard.Helpers;
using ActuallyStandard.Localization;
using ActuallyStandard.Models;
using ActuallyStandard.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;


namespace ActuallyStandard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration,
                                IStringLocalizer<SharedResources> localizer
                                )
        {
            _localizer = localizer;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var context = HttpContext;
            var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;

            var model = new HomeViewModel
            {
                PageTitle = _localizer[SharedResources.Sitemap.Home],
                ResourcesValue = _localizer[SharedResources.ResourceValue],
                CookieValue = requestCulture.Culture.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var defaultCookieName = _configuration.GetValue<string>(AppSettings.LocalizationDefaultCookieName);
            LocalizationHelper.SetCultureCookie(HttpContext, defaultCookieName, culture);

            return LocalRedirect(returnUrl);
        }

        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
