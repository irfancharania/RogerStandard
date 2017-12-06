using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActuallyStandard.Localization;
using ActuallyStandard.Services;
using ActuallyStandard.ViewModels;
using AutoMapper;
using Changelog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ActuallyStandard.Controllers
{
    [Route("api/v1")]
    public class ApiController : Controller
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IChangelogData _changelogData;
        private readonly IMapper _mapper;

        public ApiController(IStringLocalizer<SharedResources> localizer,
                             IChangelogData changelogData,
                             IMapper mapper)
        {
            _localizer = localizer;
            _changelogData = changelogData;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ReleaseViewModel>), (int)HttpStatusCode.OK)]
        public IActionResult Changelog()
        {
            var model = _mapper.Map<IEnumerable<ReleaseViewModel>>(_changelogData.GetAll());
            return Json(model);
        }

        [HttpGet("[action]/{version}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseViewModel), (int)HttpStatusCode.OK)]
        public IActionResult Changelog(string version)
        {
            var model = _mapper.Map<ReleaseViewModel>(_changelogData.Get(version));
            return Json(model);
        }

        [HttpPost("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseViewModel), (int)HttpStatusCode.OK)]
        public IActionResult CreateRelease(ReleaseViewModel model)
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
                return BadRequest(ModelState);
            }
            else
            {
                var release = Dtos.ReleaseDtoModule.fromDomain(result.ResultValue);
                _changelogData.Create(release);
                return Ok(release);
            }
        }

        [HttpGet("[action]/{version}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseViewModel), (int)HttpStatusCode.OK)]
        public IActionResult EditRelease(string version, ReleaseViewModel model) => Ok();
    }
}
