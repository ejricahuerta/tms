using System.Threading.Tasks;

namespace Infrastrucure.Security
{
    public interface IAuthentication
    {
        Task<bool> ValidateUser(string username, string password);
        Task<bool> SignInUser(string username);
    }
}
