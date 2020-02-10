using Core.Enums;
using Data.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserManager
    {
        Task<IList<User>> GetAllUsersAsync(string sort = null, SortOrder sortOrder = SortOrder.Ascending);

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByNameAsync(string name);

        Task<User> GetUserByEmailAsync(string email);
    }
}
