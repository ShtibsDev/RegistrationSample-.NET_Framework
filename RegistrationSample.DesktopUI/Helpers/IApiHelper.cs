using RegistrationSample.DesktopUI.Models;
using System.Threading.Tasks;

namespace RegistrationSample.DesktopUI.Helpers
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}