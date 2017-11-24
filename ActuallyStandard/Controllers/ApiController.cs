using Microsoft.AspNetCore.Mvc;
using ActuallyStandard.Services;
using ActuallyStandard.ViewModels;

namespace ActuallyStandard.Controllers
{
    [Route("api/v1")]
    public class ApiController : Controller
    {
        private IChangelogData _changelogData;

        public ApiController(IChangelogData changelogData)
        {
            _changelogData = changelogData;
        }

        [HttpGet("[action]")]
        public IActionResult Changelog()
        {
            var model = new ChangelogViewModel()
            {
                Releases = _changelogData.GetAll()
            };
            return Json(model);
        }

        [HttpGet("[action]/{version}")]

        public IActionResult Changelog(string version)
        {
            var change = _changelogData.Get(version);
            return Json(change);
        }
    }
}
