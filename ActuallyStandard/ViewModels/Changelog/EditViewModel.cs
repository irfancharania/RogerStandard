using System.Collections.Generic;
using ActuallyStandard.ViewModels;
using Changelog;

namespace ActuallyStandard.ViewModels.Changelog
{
    public class EditViewModel
    {
        public string PageTitle { get; set; }
        public ReleaseViewModel Release { get; set; }
    }
}
