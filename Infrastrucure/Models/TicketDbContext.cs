using Data.Entities.Tickets;
using Data.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucure.Models
{
    public class TicketDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }


        public TicketDbContext()
        {
        }

        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            optionsBuilder.UseSqlServer("Data Source=ncdelta;Initial Catalog=TICKET_IMPORT2;User ID=ericahuerta;Password=nephro"); ;
            base.OnConfiguring(optionsBuilder);
        }
    }
}
