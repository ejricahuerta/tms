using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TicketViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Ticket #")]
        public int Tag { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Resolution { get; set; }

        [Display(Name = "Reported By")]
        public string ReportedBy { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }


        public string Status { get; set; }
        public int StatusId { get; set; }

        public int PriorityId { get; set; }

        public string Priority { get; set; }

        [Display(Name = "Created By")]

        public string BelongsToName { get; set; }
        public int BelongsToId { get; set; }
        [Display(Name = "Assigned To")]
        public string AssignedToName { get; set; }
        public int AssignedToId { get; set; }
    }
}
