using Core.DataTransferObjects;
using System.Collections.Generic;

namespace Web.Models
{
    public class TicketsBodyViewModel
    {
        public IList<TicketListItemDto> TicketList { get; set; } = new List<TicketListItemDto>();
    }
}
