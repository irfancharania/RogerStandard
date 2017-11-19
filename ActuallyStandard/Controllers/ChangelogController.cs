using Microsoft.AspNetCore.Mvc;
using Standard.Services;
using Standard.ViewModels;

namespace Standard.Controllers
{
    [Route("[controller]")]
    public class ChangelogController : Controller
    {
        private IChangelogData _changelogData;

        public ChangelogController(IChangelogData changelogData)
        {
            _changelogData = changelogData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ChangelogViewModel()
            {
                PageTitle = "Changelog",
                Releases = _changelogData.GetAll()                
            };
            return View(model);
        }

        [HttpGet("[action]")]
        public IActionResult Change()
        {
            var model = new ChangelogViewModel()
            {
                PageTitle = "Changelog",
                Releases = _changelogData.GetAll()
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
