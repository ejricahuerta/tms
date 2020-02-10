using Data.Entities.Requests;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucure.Models
{
    public class RequestDbContext : DbContext
    {

        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }
        public DbSet<AXRequest> AXRequests { get; set; }
        public DbSet<AXRequestAccess> AXRequestAccesses { get; set; }
        public DbSet<EmployeeRequest> EmployeeRequests { get; set; }
        public DbSet<ITRequest> ITRequests { get; set; }



        public RequestDbContext()
        {

        }

        public RequestDbContext(DbContextOptions<RequestDbContext> options) : base(options)
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=ncdelta;Initial Catalog=TICKET_REQUEST_TEST;User ID=ericahuerta;Password=nephro"); ;
            base.OnConfiguring(optionsBuilder);
        }

    }
}
