using System;

namespace Core.DataTransferObjects
{
    public class TicketListItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }
        public int? StatusId { get; set; }

        public int? PriorityId { get; set; }
        public string Priority { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? ResolveDate { get; set; }

        public string ReportedBy { get; set; }

    }
}
