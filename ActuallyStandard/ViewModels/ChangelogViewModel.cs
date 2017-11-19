using System.Collections.Generic;
using Changelog;

namespace Standard.ViewModels
{
    public class ChangelogViewModel
    {
        public string PageTitle { get; set; }
        public IEnumerable<Dtos.ReleaseDto> Releases { get; set; }
    }
}
