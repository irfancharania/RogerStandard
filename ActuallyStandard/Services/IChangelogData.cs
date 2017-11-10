using System.Collections.Generic;
using static Changelog.Dtos;

namespace Standard.Services
{
    public interface IChangelogData
    {
        IEnumerable<ReleaseDto> GetAll();
        ReleaseDto Get(string version);
        

    }
}
