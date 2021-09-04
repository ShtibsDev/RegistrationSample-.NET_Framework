using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.Library.API
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
        private ILoggedInUserModel _logedInUser;

        public ApiHelper(ILoggedInUserModel logedInUser)
        {
            InitializeClient();
            _logedInUser = logedInUser; 
        }

        private void InitializeClient()
        {
            var apiUri = ConfigurationManager.AppSettings["API"];
            _apiClient = new HttpClient { BaseAddress = new Uri(apiUri) };
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string> ("grant_type", "password"),
                new KeyValuePair<string, string> ("username", username),
                new KeyValuePair<string, string> ("password", password)
            });

            using (var response = await _apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    _logedInUser.Token = result.Access_token;
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task GetLogedInUserInfo()
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_logedInUser.Token}");

            using (var response = await _apiClient.GetAsync("api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LogedInUserModel>();
                    _logedInUser.Id = result.Id;
                    _logedInUser.FirstName = result.FirstName;
                    _logedInUser.LastName = result.LastName;
                    _logedInUser.BirthDate = result.BirthDate;
                    _logedInUser.EmailAddress = result.EmailAddress;
                    _logedInUser.LastLogin = result.LastLogin;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}