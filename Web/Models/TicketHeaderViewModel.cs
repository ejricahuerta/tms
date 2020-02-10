using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TicketsHeaderViewModel
    {
        public Dictionary<string, string> TicketHeaderItems { get; set; } = new Dictionary<string, string>();
        [Display(Name = "Created By")]
        public List<SelectListItem> CreatedBy { get; set; } = new List<SelectListItem>();
        [Display(Name = "Assigned To")]
        public List<SelectListItem> AssignedTo { get; set; } = new List<SelectListItem>();
        [Display(Name = "Reported By")]
        public List<SelectListItem> ReportedBy { get; set; } = new List<SelectListItem>();
        public List<string> PropertySort { get; set; } = new List<string>();

        public List<SelectListItem> Priorities { get; set; } = new List<SelectListItem>();

        public FilterViewModel Filter { get; set; }
    }
}
