using System.Collections.Generic;
using ActuallyStandard.ViewModels;
using Changelog;

namespace ActuallyStandard.ViewModels
{
    public class ChangelogViewModel
    {
        public string PageTitle { get; set; }
        public IEnumerable<ReleaseViewModel> Releases { get; set; }
    }
}
