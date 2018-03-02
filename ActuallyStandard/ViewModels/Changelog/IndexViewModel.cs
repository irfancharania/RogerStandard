using System.Collections.Generic;

namespace ActuallyStandard.ViewModels.Changelog
{
    public class IndexViewModel : BaseViewModel
    {
        public IEnumerable<ReleaseViewModel> Releases { get; set; }
    }
}
