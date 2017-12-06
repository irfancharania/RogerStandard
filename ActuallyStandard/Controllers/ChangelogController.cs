using System.Collections.Generic;
using System.Net;
using ActuallyStandard.Localization;
using ActuallyStandard.Services;
using ActuallyStandard.ViewModels;
using ActuallyStandard.ViewModels.Changelog;
using AutoMapper;
using Changelog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace ActuallyStandard.Controllers
{
    [Route("[controller]")]
    public class ChangelogController : Controller
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IChangelogData _changelogData;
        private readonly IMapper _mapper;
        private readonly ILogger<ChangelogController> _logger;

        public ChangelogController(IStringLocalizer<SharedResources> localizer
                                  , IChangelogData changelogData
                                  , IMapper mapper
                                  , ILogger<ChangelogController> logger)
        {
            _localizer = localizer;
            _changelogData = changelogData;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var model = new IndexViewModel()
            {
                PageTitle = _localizer[SharedResources.Sitemap.Changelog],
                Releases = _mapper.Map<IEnumerable<ReleaseViewModel>>(_changelogData.GetAll())
            };

            _logger.LogDebug("Changelog - Get results for {title}", model.PageTitle);

            return View(model);
        }

        [HttpGet("[action]/{version}")]
        public ViewResult Details(string version)
        {
            var model = new DetailViewModel()
            {
                PageTitle = "Details - Version " + version,
                Release = _mapper.Map<ReleaseViewModel>(_changelogData.Get(version))
            };
            return View(model);
        }

        [HttpGet("[action]")]
        public ViewResult Create() => View();

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReleaseCreateViewModel model)
        {
            var dto = _mapper.Map<Dtos.ReleaseDto>(model);
            var result = Dtos.ReleaseDtoModule.toDomain(dto);

            if (result.IsError)
            {
                foreach (var domainMessage in result.ErrorValue)
                {
                    var key = string.Concat(nameof(SharedResources.Error), ".", domainMessage.ToString());
                    ModelState.AddModelError(string.Empty, _localizer[key]);
                }
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var release = Dtos.ReleaseDtoModule.fromDomain(result.ResultValue);
                _changelogData.Create(release);
                return RedirectToAction(nameof(Details), release.ReleaseVersion);
            }
        }
    }
}
