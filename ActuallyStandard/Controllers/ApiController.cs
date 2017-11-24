using System.Collections.Generic;
using ActuallyStandard.Localization;
using ActuallyStandard.Services;
using ActuallyStandard.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ActuallyStandard.Controllers
{
    [Route("api/v1")]
    public class ApiController : Controller
    {
        private IStringLocalizer<SharedResources> _localizer;
        private IChangelogData _changelogData;
        private IMapper _mapper;

        public ApiController(IStringLocalizer<SharedResources> localizer,
                             IChangelogData changelogData,
                             IMapper mapper)
        {
            _localizer = localizer;
            _changelogData = changelogData;
            _mapper = mapper;            
        }

        //private void ConfigureMapping() => 
        //    Mapper.WhenMapping
        //            .From<Dtos.WorkItemDto>()
        //            .To<WorkItemViewModel>()
        //            .Map(ctx => GetLocalizedString(_localizer
        //                                            , typeof(Dtos.WorkItemTypeDto)
        //                                            , ctx.Source.WorkItemType))
        //            .To(dto => dto.WorkItemTypeString);
        
        //private string GetLocalizedString(IStringLocalizer localizer, Type type, int value) {
        //    var localized = localizer[$"{type.Name}_{Enum.GetName(type, value)}"];
        //    return localized;
        //}

        [HttpGet("[action]")]
        public IActionResult Changelog()
        {
            var model = new ChangelogViewModel()
            {
                Releases = _mapper.Map<IEnumerable<ReleaseViewModel>>(_changelogData.GetAll())
                                 
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
