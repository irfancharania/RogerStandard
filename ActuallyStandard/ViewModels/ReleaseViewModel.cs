using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActuallyStandard.ViewModels
{
    public class ReleaseViewModel
    {
        public Guid ReleaseId { get; set; }
        [Required]
        public string ReleaseVersion { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Authors { get; set; }
        public List<WorkItemViewModel> WorkItems { get; set; }
        public byte[] RecordVersion { get; set; }
    }

    public class WorkItemViewModel
    {
        public int WorkItemId { get; set; }
        public int WorkItemType { get; set; }
        [Required]
        public string Description { get; set; }
        public string WorkItemTypeString { get; set; }
    }
}
