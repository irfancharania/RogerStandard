using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuallyStandard.ViewModels.Changelog
{
    public class FeedViewModel
    {
        public FeedViewModel(string title, Guid id, DateTimeOffset updatedDate)
        {
            Title = title;
            Id = id;
            UpdatedDate = updatedDate;
        }
        public string Title { get; set; }
        public Guid Id { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public List<ReleaseViewModel> Items { get; set; }
    }
}
