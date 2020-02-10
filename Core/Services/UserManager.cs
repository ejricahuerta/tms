using Core.Enums;
using Core.Interfaces;
using Data.Entities.Users;
using Infrastrucure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserManager : IUserManager
    {
        private readonly TicketDbContext context;

        public UserManager(TicketDbContext context)
        {
            this.context = context;
        }

        public Task<IList<User>> GetAllUsersAsync(string sort = null, SortOrder sortOrder = SortOrder.Ascending)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
