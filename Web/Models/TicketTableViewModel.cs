using System.Collections.Generic;

namespace Web.Models
{
    public class TicketTicketViewModel
    {
        public bool HasHeader { get; set; }
        public List<TicketViewModel> Tickets { get; set; }
    }
}
