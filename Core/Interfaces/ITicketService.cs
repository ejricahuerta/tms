using Core.DataTransferObjects;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITicketService
    {
        Task<IList<TicketListItemDto>> GetAllTicketsAsync(int? status = null, int? priorityId = null, int? createdBy = null, int? assignedTo = null, string reportedBy = null, string keyword = null, DateTime? dateFrom = null, DateTime? dateTo = null, string sort = null, SortOrder order = SortOrder.Ascending);
        Task<IList<string>> GetAllNamesOfReportedBy();
        Task<TicketDto> GetTicketAsync(int ticketId);

    }
}
