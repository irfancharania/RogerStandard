using System.Collections.Generic;

namespace ActuallyStandard.ViewModels.Changelog
{
    public class IndexViewModel
    {
        public string PageTitle { get; set; }
        public IEnumerable<ReleaseViewModel> Releases { get; set; }
    }
}
