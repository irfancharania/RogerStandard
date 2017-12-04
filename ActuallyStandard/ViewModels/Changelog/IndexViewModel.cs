using System.Collections.Generic;
using ActuallyStandard.ViewModels;
using Changelog;

namespace ActuallyStandard.ViewModels.Changelog
{
    public class IndexViewModel
    {
        public string PageTitle { get; set; }
        public IEnumerable<ReleaseViewModel> Releases { get; set; }
    }
}
