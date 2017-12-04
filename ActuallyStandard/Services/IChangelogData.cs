using System.Collections.Generic;
using static Changelog.Dtos;

namespace ActuallyStandard.Services
{
    public interface IChangelogData
    {
        IEnumerable<ReleaseDto> GetAll();
        ReleaseDto Get(string version);

        void CreateRelease(ReleaseDto release);

    }
}
