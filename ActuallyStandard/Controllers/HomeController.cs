using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ActuallyStandard.Constants;
using ActuallyStandard.Helpers;
using ActuallyStandard.Localization;
using ActuallyStandard.Models;
using ActuallyStandard.Services;
using ActuallyStandard.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;


namespace ActuallyStandard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IConfiguration _configuration;
        private readonly IFeedService _feedService;
        private readonly IChangelogData _changelogData;
        private readonly IMapper _mapper;

        public HomeController(IConfiguration configuration,
                                IHostingEnvironment env,
                                IChangelogData changelogData,
                                IMapper mapper,
                                IStringLocalizer<SharedResources> localizer,
                                IFeedService feedService)
        {
            _env = env;
            _localizer = localizer;
            _configuration = configuration;
            _feedService = feedService;
            _mapper = mapper;
            _changelogData = changelogData;
        }

        public IActionResult Index()
        {
            var context = HttpContext;
            var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;

            var model = new HomeViewModel
            {
                ResourcesValue = _localizer[SharedResources.ResourceValue],
                CookieValue = requestCulture.Culture.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var defaultCookieName = _configuration.GetValue<string>(AppSettings.Localization_DefaultCookieName);
            LocalizationHelper.SetCultureCookie(HttpContext, defaultCookieName, culture);

            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 10000)]
        public async Task<ActionResult> Feed()
        {
            var releases = _mapper.Map<IEnumerable<ReleaseViewModel>>(_changelogData.GetAll());
            return Content(await _feedService.GenerateFeed(releases), "application/atom+xml");
        }

        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
