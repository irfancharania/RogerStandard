using System.Collections.Generic;
using System.Net;
using ActuallyStandard.Services;
using ActuallyStandard.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ActuallyStandard.Controllers
{
    [Route("[controller]")]
    public class ChangelogController : Controller
    {
        private IChangelogData _changelogData;
        private IMapper _mapper;

        public ChangelogController(IChangelogData changelogData, 
                                    IMapper mapper)
        {
            _changelogData = changelogData;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ChangelogViewModel()
            {
                PageTitle = "Changelog",
                Releases = _mapper.Map<IEnumerable<ReleaseViewModel>>(_changelogData.GetAll())
            };
            return View(model);
        }

        [HttpGet("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ChangelogViewModel), (int)HttpStatusCode.OK)]
        public IActionResult Change()
        {
            var model = new ChangelogViewModel()
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
