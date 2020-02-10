using Core.DataTransferObjects;
using Core.Interfaces;
using Data.Entities.Tickets;
using Infrastrucure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.Enums;
using Web.Models;

namespace Web.Pages
{
    public class TicketsModel : PageModel
    {
        #region InjectedProperties
        private readonly ITicketService ticketService;
        private readonly TicketDbContext context;
        #endregion


        #region FilterLists


        public static SelectListItem DefaultListItem { get; set; } = new SelectListItem("All", "0");
        [Display(Name = "Created By")]
        public List<SelectListItem> CreatedBy { get; set; } = new List<SelectListItem>();
        [Display(Name = "Assigned To")]
        public List<SelectListItem> AssignedTo { get; set; } = new List<SelectListItem>();
        [Display(Name = "Reported By")]
        public List<SelectListItem> ReportedBy { get; set; } = new List<SelectListItem>();

        public List<string> PropertySort { get; set; } = new List<string>();

        public List<SelectListItem> Priorities { get; set; } = new List<SelectListItem>();
        #endregion

        #region ModelPropeties

        public TicketsHeaderViewModel TicketHeader { get; set; } = new TicketsHeaderViewModel();
        public TicketsBodyViewModel TicketBody { get; set; } = new TicketsBodyViewModel();

        [BindProperty(SupportsGet = true)]
        public FilterViewModel Filter { get; set; }


        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

        public Dictionary<string, string> TicketHeaderItems { get; set; } = new Dictionary<string, string>();
        public IList<TicketDto> TicketList { get; set; } = new List<TicketDto>();
        public string Keyword { get; set; }

        public List<Status> Statuses { get; set; } = new List<Status>();
        public Status DefaultStatus { get; set; } = new Status
        {
            Id = 0,
            Name = "All",
        };

        public int ActiveStatusId { get; set; } = 0;

        #endregion

        public TicketsModel(ITicketService ticketService, TicketDbContext context)
        {
            this.ticketService = ticketService;
            this.context = context;
        }

        public async Task<IActionResult> OnGet(int? statusId = null)
        {
            TicketHeader = new TicketsHeaderViewModel();
            TicketBody = new TicketsBodyViewModel();

            await GenerateList(statusId);

            TicketHeader.Filter = Filter ?? new FilterViewModel();

            return Page();
        }

        public async Task<IActionResult> OnGetBody(int? statusId = null)
        {
            IList<TicketListItemDto> ticketResult;
            TicketBody = new TicketsBodyViewModel();

            if (Filter != null)
            {
                ticketResult = await ticketService.GetAllTicketsAsync(Filter.StatusId, Filter.PriorityId, Filter.CreatedById, Filter.AssignedToId, Filter.ReportedBy, Filter.Keyword, null, null, null);
            }
            else
            {
                ticketResult = await ticketService.GetAllTicketsAsync();
            }

            TicketBody.TicketList = ticketResult;

            bool valid = true;

            if (valid)
            {

                return new PartialViewResult
                {
                    ViewName = "_TicketBodyPartial",
                    ViewData = new ViewDataDictionary<TicketsBodyViewModel>(ViewData, TicketBody)

                };
            }
            else
            {
                return new PartialViewResult
                {
                    ViewName = "_ErrorPartial",
                    ViewData = new ViewDataDictionary<ErrorViewModel>(ViewData, new ErrorViewModel())

                };
            }
        }
        public async Task<IActionResult> OnGetHeader()
        {

            TicketHeader = new TicketsHeaderViewModel();

            var reportedByResult = await ticketService.GetAllNamesOfReportedBy();

            System.Console.WriteLine();
            System.Console.WriteLine("Reported By Names...");
            foreach (var item in reportedByResult)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine();
            System.Console.WriteLine();



            //TicketBody.TicketList = ticketResult;

            TicketHeader.ReportedBy.Add(DefaultListItem);

            TicketHeader.ReportedBy.AddRange(reportedByResult.Select(p => new SelectListItem
            {
                Selected = p.Equals(Filter.ReportedBy),
                Text = p,
                Value = p

            }).ToList());


            TicketHeader.PropertySort = TicketPropertyNames().ToList();

            TicketHeader.AssignedTo.Add(DefaultListItem);

            TicketHeader.AssignedTo.AddRange(await context.Users.Select(p => new SelectListItem
            {
                Selected = p.Id == Filter.AssignedToId,
                Text = $"{p.FirstName} {p.LastName}",
                Value = p.Id.ToString()
            }).ToListAsync());

            TicketHeader.CreatedBy.Add(DefaultListItem);

            TicketHeader.CreatedBy.AddRange(await context.Users.Select(p => new SelectListItem
            {
                Selected = p.Id == Filter.CreatedById,
                Text = $"{p.FirstName} {p.LastName}",
                Value = p.Id.ToString()
            }).ToListAsync());

            TicketHeader.Priorities.Add(DefaultListItem);
            TicketHeader.Priorities = await context.Priorities.Select(p => new SelectListItem
            {
                Selected = p.Id == Filter.PriorityId,
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToListAsync();


            bool valid = true;

            if (valid)
            {

                return new PartialViewResult
                {
                    ViewName = "_TicketHeaderPartial",
                    ViewData = new ViewDataDictionary<TicketsHeaderViewModel>(ViewData, TicketHeader)

                };
            }
            else
            {
                return new PartialViewResult
                {
                    ViewName = "_ErrorPartial",
                    ViewData = new ViewDataDictionary<ErrorViewModel>(ViewData, new ErrorViewModel())

                };
            }
        }

        public async Task<IActionResult> OnGetDetail(int ticketId)
        {
            TicketDto ticket = null;
            try
            {
                ticket = await ticketService.GetTicketAsync(ticketId);

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Unable to fetch Ticket");
                System.Console.WriteLine($"Error: {e.Message}");
                System.Console.WriteLine($"Error Stack Trace: {e.StackTrace}");
            }

            if (ticket != null)
            {
                return new PartialViewResult
                {
                    ViewName = "_TicketDetailPartial",
                    ViewData = new ViewDataDictionary<TicketDto>(ViewData, ticket)

                };
            }
            else
            {
                return new PartialViewResult
                {
                    ViewName = "_ErrorPartial",
                    ViewData = new ViewDataDictionary<ErrorViewModel>(ViewData, new ErrorViewModel())

                };
            }


        }



        private async Task GenerateList(int? statusId = null)
        {
            var allStatus = await context.Statuses.ToListAsync();

            Statuses.AddRange(allStatus.OrderBy(p => p.Name));

            Statuses.Add(DefaultStatus);
            if (!statusId.HasValue)
            {
                var defaultStats = Statuses.FirstOrDefault(p => p.Name.Equals("Assigned"));
                if (defaultStats != null)
                {
                    ActiveStatusId = defaultStats.Id;
                }
            }
            else
            {
                ActiveStatusId = statusId.Value;
            }


            IList<TicketListItemDto> ticketResult = new List<TicketListItemDto>();

            ticketResult = await ticketService.GetAllTicketsAsync(ActiveStatusId);

            if (ticketResult.Any())
            {
                TicketBody.TicketList = ticketResult;

            }

            TicketHeader.ReportedBy.Add(DefaultListItem);

            var reportedByList = await ticketService.GetAllNamesOfReportedBy();

            TicketHeader.ReportedBy.AddRange(reportedByList.Select(p => new SelectListItem
            {
                Selected = p.Equals(Filter.ReportedBy),
                Text = p,
                Value = p
            }).ToList());

            TicketHeader.PropertySort = TicketPropertyNames().ToList();

            TicketHeader.AssignedTo.Add(DefaultListItem);

            TicketHeader.AssignedTo.AddRange(await context.Users.Select(p => new SelectListItem
            {
                Selected = p.Id == Filter.AssignedToId,
                Text = $"{p.FirstName} {p.LastName}",
                Value = p.Id.ToString()
            }).ToListAsync());

            TicketHeader.CreatedBy.Add(DefaultListItem);

            TicketHeader.CreatedBy.AddRange(await context.Users.Select(p => new SelectListItem
            {
                Selected = p.Id == Filter.CreatedById,
                Text = $"{p.FirstName} {p.LastName}",
                Value = p.Id.ToString()
            }).ToListAsync());

            TicketHeader.Priorities.Add(DefaultListItem);
            TicketHeader.Priorities = await context.Priorities.Select(p => new SelectListItem
            {
                Selected = p.Id == Filter.PriorityId,
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToListAsync();
        }
        private List<string> TicketPropertyNames()
        {
            var props = new List<string>();

            var allprops = typeof(TicketViewModel).GetProperties();
            foreach (var item in allprops)
            {
                var displayName = item.GetCustomAttribute(typeof(DisplayAttribute), true) as DisplayAttribute;
                if (displayName != null)
                {
                    System.Console.WriteLine(displayName.Name);
                    props.Add(displayName.Name);

                }
                else
                {
                    if (!item.Name.Contains("Id"))
                    {

                        System.Console.WriteLine(item.Name);
                        props.Add(item.Name);
                    }
                }
            }
            return props;
        }

    }
}