using System;
using Web.Enums;

namespace Web.Models
{
    public class FilterViewModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public int StatusId { get; set; } = 0;
        public int PriorityId { get; set; } = 0;
        public int CreatedById { get; set; } = 0;
        public string ReportedBy { get; set; }
        public int AssignedToId { get; set; } = 0;
        public string Keyword { get; set; }
        public string Sort { get; set; }
        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
    }
}
