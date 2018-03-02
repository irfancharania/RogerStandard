using System.Collections.Generic;
using System.Threading.Tasks;
using ActuallyStandard.Constants;
using ActuallyStandard.Services;
using ActuallyStandard.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ActuallyStandard.Controllers
{
    public class FeedController : Controller
    {
        private readonly IFeedService _feedService;
        private readonly IChangelogData _changelogData;
        private readonly IMapper _mapper;

        public FeedController(IChangelogData changelogData,
                                IMapper mapper,
                                IFeedService feedService)
        {
            _feedService = feedService;
            _mapper = mapper;
            _changelogData = changelogData;
        }

        [ResponseCache(Duration = 10000, VaryByQueryKeys = new[] { Config.LocalizationDefaultQueryStringParameter })]
        public async Task<ActionResult> Index()
        {
            var releases = _mapper.Map<IEnumerable<ReleaseViewModel>>(_changelogData.GetLatest(Config.FeedMaxEntriesToDisplay));
            return Content(await _feedService.GenerateFeed(releases), "application/atom+xml; charset=utf8");
        }
    }
}
