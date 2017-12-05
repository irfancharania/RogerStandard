using System.Collections.Generic;
using System.Net;
using ActuallyStandard.Services;
using ActuallyStandard.ViewModels;
using ActuallyStandard.ViewModels.Changelog;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ActuallyStandard.Controllers
{
    [Route("[controller]")]
    public class ChangelogController : Controller
    {
        private readonly IChangelogData _changelogData;
        private readonly IMapper _mapper;
        private readonly ILogger<ChangelogController> _logger;

        public ChangelogController(IChangelogData changelogData
                                  , IMapper mapper
                                  , ILogger<ChangelogController> logger)
        {
            _changelogData = changelogData;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                PageTitle = "Changelog",
                Releases = _mapper.Map<IEnumerable<ReleaseViewModel>>(_changelogData.GetAll())
            };

            _logger.LogDebug("Changelog - Get results for {title}", model.PageTitle);

            return View(model);
        }

        [HttpGet("[action]/{version}")]
        public IActionResult Details(string version)
        {
            var model = new DetailViewModel()
            {
                PageTitle = "Details - Version " + version,
                Release = _mapper.Map<ReleaseViewModel>(_changelogData.Get(version))
            };
            return View(model);
        }

        [HttpGet("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IndexViewModel), (int)HttpStatusCode.OK)]
        public IActionResult Change()
        {
            var model = new IndexViewModel()
            {
                PageTitle = "Changelog",
                Releases = _mapper.Map<IEnumerable<ReleaseViewModel>>(_changelogData.GetAll())
            };
            return Json(model);
        }

        [HttpGet("[action]/{version}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseViewModel), (int)HttpStatusCode.OK)]
        public IActionResult Change(string version)
        {
            var model = _mapper.Map<ReleaseViewModel>(_changelogData.Get(version));
            return Json(model);
        }
    }
}
