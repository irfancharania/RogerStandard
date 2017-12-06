using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActuallyStandard.ViewModels
{
    public class ReleaseViewModel
    {
        public Guid ReleaseId { get; set; }
        public string ReleaseVersion { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Authors { get; set; }
        public List<WorkItemViewModel> WorkItems { get; set; }
        public byte[] RecordVersion { get; set; }
    }

    public class ReleaseCreateViewModel
    {
        [Required]
        public string ReleaseVersion { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public List<string> Authors { get; set; }
        [Required]
        public List<WorkItemViewModel> WorkItems { get; set; }
    }
    public class ReleaseEditViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public List<string> Authors { get; set; }
        [Required]
        public List<WorkItemViewModel> WorkItems { get; set; }
    }
    public class WorkItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public int WorkItemType { get; set; }
        [Required]
        public string Description { get; set; }
        public string WorkItemTypeString { get; set; }
    }
    public class WorkItemEditViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int WorkItemType { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
