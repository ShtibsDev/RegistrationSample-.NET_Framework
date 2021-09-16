using System.Threading.Tasks;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.Library.API
{
    public interface IUserEndpoint
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task<LoggedInUserModel> GetLogedInUserInfo();
        Task RegisterUser(NewUserModel newUser);
        Task Update(LoggedInUserModel user);
        Task LogUserOut();
    }
}