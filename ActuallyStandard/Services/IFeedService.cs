using System.Collections.Generic;
using System.Threading.Tasks;
using ActuallyStandard.ViewModels;

namespace ActuallyStandard.Services
{
    public interface IFeedService
    {
        Task<string> GenerateFeed(IEnumerable<ReleaseViewModel> releases);
    }
}
