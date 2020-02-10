using Core.DataTransferObjects;
using Core.Enums;
using Core.Interfaces;
using Data.Entities.Tickets;
using Infrastrucure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly TicketDbContext context;
        private readonly IMemoryCache cache;

        public TicketService(TicketDbContext context, IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;

        }

        public async Task<IList<string>> GetAllNamesOfReportedBy()
        {
            List<string> reportedName = new List<string>();

            try
            {
                var result = await context.Tickets.Select(p => p.ReportedBy).GroupBy(p => p).Select(p => p.First()).ToListAsync();

                if (result.Any())
                {
                    reportedName = result;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to process");
            }

            return reportedName;

        }

        public async Task<IList<TicketListItemDto>> GetAllTicketsAsync(int? statusId = null, int? priorityId = null, int? createdBy = null, int? assignedTo = null, string reportedBy = null, string keyword = null, DateTime? dateFrom = null, DateTime? dateTo = null, string sort = null, SortOrder order = SortOrder.Ascending)
        {
            var key = $"tickets_{statusId}{priorityId}{createdBy}{assignedTo}{reportedBy}{keyword}";


            //  var cached = await cache.GetOrCreateAsync<IQueryable<Ticket>>(key, entry =>
            //{
            //    return Task.FromResult(context.Tickets.AsNoTracking());
            //});

            IQueryable<Ticket> result = context.Tickets.AsNoTracking().OrderByDescending(p => p.Id).AsQueryable();


            try
            {
                if (statusId.HasValue && statusId != 0)
                {
                    result = result.Where(p => p.StatusId == statusId);
                }

                if (priorityId.HasValue && priorityId != 0)
                {
                    result = result.Where(p => p.PriorityId == priorityId);
                }

                if (createdBy.HasValue && createdBy != 0)
                {
                    result = result.Where(p => p.BelongsToId == createdBy);
                }
                else if (assignedTo.HasValue && assignedTo != 0)
                {
                    result = result.Where(p => p.AssignedToId == assignedTo);

                }
                else if (!string.IsNullOrEmpty(reportedBy) && !reportedBy.Equals("0"))
                {
                    result = result.Where(p => p.ReportedBy.ToLower().Equals(reportedBy.ToLower()));

                }
                else if (!string.IsNullOrEmpty(keyword))
                {
                    result = result.Where(p => p.Description.Contains(keyword) || (p.Resolution != null && p.Resolution.Contains(keyword)));
                }

                if (dateFrom.HasValue && dateFrom.HasValue)
                {
                    //From and To Date 
                    result = result.Where(p => p.DateAdded > dateFrom && p.DateAdded < dateTo);
                }
                else if (!dateFrom.HasValue && dateFrom.HasValue)
                {
                    //Only To Date
                    result = result.Where(p => p.DateAdded < dateTo);
                }
                //
                else if (dateFrom.HasValue && !dateFrom.HasValue)
                {
                    result = result.Where(p => p.DateAdded > dateFrom);

                }

                if (!string.IsNullOrEmpty(sort))
                {
                    switch (order)
                    {
                        case SortOrder.Ascending:

                            //Ascending
                            break;
                        case SortOrder.Descending:
                            //Descending

                            break;
                        default:
                            result = result.OrderBy(p => p.Id);
                            break;
                    }
                }

                var list = await result.Select(p => new TicketListItemDto
                {

                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    DateAdded = p.DateAdded,
                    DateModified = p.DateModified,
                    ResolveDate = p.ResolveDate,
                    ReportedBy = p.ReportedBy,

                    //Navigation Properties

                    Status = p.Status.Name,
                    StatusId = p.StatusId.Value,

                    Priority = p.PriorityId.HasValue ? p.Priority.Name : string.Empty,
                    PriorityId = p.PriorityId,
                }).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to Fetch tickets. Returned null");
                Console.WriteLine($"Error : {e.Message}");
                Console.WriteLine($"Error Stack Trace: {e.StackTrace}");
                return null;
            }
        }

        public async Task<TicketDto> GetTicketAsync(int ticketId)
        {
            TicketDto ticket = null;
            try
            {
                ticket = await context.Tickets.Select(p => new TicketDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Resolution = p.Resolution,
                    DateAdded = p.DateAdded,
                    DateModified = p.DateModified,
                    ResolveDate = p.ResolveDate,
                    ReportedBy = p.ReportedBy,


                    //Navigation Properties

                    AssignedToId = p.AssignedToId,
                    AssignedToUsername = p.AssignedToId.HasValue ? p.AssignedTo.Username : string.Empty,

                    SiteId = p.SiteId,
                    SiteName = p.Site.Name,

                    Status = p.StatusId.HasValue ? p.Status.Name : string.Empty,
                    StatusId = p.StatusId,

                    BelongsToId = p.BelongsToId,
                    BelongsToUsername = p.BelongsToId.HasValue ? p.BelongsTo.Username : string.Empty,

                    Priority = p.PriorityId.HasValue ? p.Priority.Name : string.Empty,
                    PriorityId = p.PriorityId,


                }).FirstOrDefaultAsync(p => p.Id == ticketId);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to get ticket# {ticketId}");
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error Stack Trace: {e.StackTrace}");

            }
            return ticket;
        }
    }
}
