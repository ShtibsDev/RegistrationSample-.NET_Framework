using System.Net.Http.Headers;

namespace RegistrationSample.DesktopUI.Library.API
{
    public class ApiHelper : IApiHelper
    {
        public ApiHelper()
        {
            InitializeClient();
        }

        public HttpClient ApiClient { get; private set; }

        private void InitializeClient()
        {
            //var apiUri = Configuration["API"];
            ApiClient = new HttpClient { BaseAddress = new Uri("https://localhost:44393/") };
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}