using System.Collections.Generic;
using static Changelog.Dtos;

namespace ActuallyStandard.Services
{
    public interface IChangelogData
    {
        IEnumerable<ReleaseDto> GetAll();
        IEnumerable<ReleaseDto> GetLatest(byte limit);
        ReleaseDto Get(string version);
        void Create(ReleaseDto release);

    }
}
