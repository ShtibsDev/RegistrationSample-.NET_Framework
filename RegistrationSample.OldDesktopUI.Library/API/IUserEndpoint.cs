using System.Threading.Tasks;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.Library.API
{
    public interface IUserEndpoint
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLogedInUserInfo();
        Task Update();
        Task LogUserOut();
    }
}