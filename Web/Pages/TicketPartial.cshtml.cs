using Infrastrucure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using Web.Models;

namespace Web.Pages
{
    public class TicketPartialModel : PageModel
    {
        private readonly TicketDbContext context;

        [BindProperty(SupportsGet = true)]
        FilterViewModel Filter { get; set; }

        public List<TicketViewModel> TicketList { get; set; } = new List<TicketViewModel>();

        public TicketsHeaderViewModel TicketHeader { get; set; }

        public TicketsBodyViewModel TicketBody { get; set; }

        public TicketPartialModel(TicketDbContext context)
        {
            this.context = context;
        }
        public IActionResult OnGetBody()
        {
            TicketBody = new TicketsBodyViewModel();

            bool valid = true;

            if (valid)
            {

                return new PartialViewResult
                {
                    ViewName = "_TicketBodyPartial",
                    ViewData = new ViewDataDictionary<TicketsBodyViewModel>(ViewData, new TicketsBodyViewModel())

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
        public IActionResult OnGetHeader()
        {
            bool valid = true;

            if (valid)
            {

                return new PartialViewResult
                {
                    ViewName = "_TicketHeaderPartial",
                    ViewData = new ViewDataDictionary<TicketHeaderItemViewModel>(ViewData, new TicketHeaderItemViewModel())

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
    }
}