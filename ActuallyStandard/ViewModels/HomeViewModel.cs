using System.ComponentModel.DataAnnotations;

namespace ActuallyStandard.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public string Locale { get; set; }

        public string CurrentDateTime { get; }

        [Display(Name = Localization.SharedResources.CookieValue)]
        public string CookieValue { get; set; }
        [Display(Name = Localization.SharedResources.ResourceValue)]
        public string ResourcesValue { get; set; }
    }
}
