using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RegistrationSample.DesktopUI.Models;

namespace RegistrationSample.DesktopUI.Helpers
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
        public ApiHelper()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            _apiClient = new();
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