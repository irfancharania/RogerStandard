using System.Threading.Tasks;

namespace ActuallyStandard.Services
{
    public interface IFeedService
    {
        Task<string> GetFeed();
    }
}
