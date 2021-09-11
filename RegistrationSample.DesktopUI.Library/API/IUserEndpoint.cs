using RegistrationSample.DesktopUI.Library.Models;

namespace RegistrationSample.DesktopUI.Library.API
{
    public interface IUserEndpoint
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLogedInUserInfo();
        Task Update();
        Task LogUserOut();
    }
}