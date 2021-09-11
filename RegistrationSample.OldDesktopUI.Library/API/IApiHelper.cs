using System.Net.Http;

namespace RegistrationSample.OldDesktopUI.Library.API
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
    }
}