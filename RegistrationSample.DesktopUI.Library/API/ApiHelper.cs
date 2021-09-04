using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RegistrationSample.DesktopUI.Library.Models;

namespace RegistrationSample.DesktopUI.Library.API
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
        private readonly IConfiguration _config;
        public ApiHelper(IConfiguration configuration)
        {
            InitializeClient();
            _config = configuration;
        }

        private void InitializeClient()
        {
            //var apiUri = _config["API"];
            _apiClient = new();
            _apiClient.BaseAddress = new Uri("https://localhost:44393/");
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new ("grant_type", "password"),
                new ("username", username),
                new ("password", password)
            });

            using var response = await _apiClient.PostAsync("/Token", data);
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<AuthenticatedUser>(stream);
                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}