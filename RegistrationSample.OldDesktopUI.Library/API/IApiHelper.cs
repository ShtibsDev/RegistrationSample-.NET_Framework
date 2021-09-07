using System.Net.Http;
using System.Threading.Tasks;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.Library.API
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
    }
}