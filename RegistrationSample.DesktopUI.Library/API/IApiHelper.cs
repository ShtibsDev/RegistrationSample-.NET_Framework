using RegistrationSample.DesktopUI.Library.Models;

namespace RegistrationSample.DesktopUI.Library.API
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}