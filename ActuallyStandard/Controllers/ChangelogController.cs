using System.Collections.Generic;
using ActuallyStandard.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Standard.Services;
using Standard.ViewModels;

namespace Standard.Controllers
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
        public IActionResult Change(string version)
        {
            var change = _changelogData.Get(version);
            return Json(change);
        }
    }
}
