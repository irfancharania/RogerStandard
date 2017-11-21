using System.ComponentModel.DataAnnotations;
using ActuallyStandard.Constants;

namespace ActuallyStandard.ViewModels
{
    public class HomeViewModel
    {
        
        public string PageTitle { get; set; }
        public string Locale { get; set; }

        public string CurrentDateTime { get; }

        [Display(Name = Localization.SharedResources.CookieValue)]
        public string CookieValue { get; set; }
        [Display(Name = Localization.SharedResources.ResourceValue)]
        public string ResourcesValue { get; set; }
    }
}
