﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.Library.API
{
    public class UserEndpoint : IUserEndpoint
    {
        private readonly IApiHelper _apiHelper;
        private readonly ILoggedInUserModel _loggedInUser;

        public UserEndpoint(IApiHelper apiHelper, ILoggedInUserModel loggedInUser)
        {
            _apiHelper = apiHelper;
            _loggedInUser = loggedInUser;
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string> ("grant_type", "password"),
                new KeyValuePair<string, string> ("username", username),
                new KeyValuePair<string, string> ("password", password)
            });

            using (var response = await _apiHelper.ApiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    _loggedInUser.Token = result.Access_token;
                    SetAuthenticationHeaders();
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
            using (var response = await _apiHelper.ApiClient.GetAsync("api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LogedInUserModel>();
                    _loggedInUser.AssignUser(result);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task Update()
        {
            var content = JsonSerializer.Serialize(_loggedInUser);
            var request = new HttpRequestMessage { Method = HttpMethod.Put, RequestUri = new Uri("api/User"), Content = new StringContent(content) };
            using (var response = await _apiHelper.ApiClient.SendAsync(request))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task LogUserOut()
        {
            using (var response = await _apiHelper.ApiClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/Account/Logout")))
            {
                if (response.IsSuccessStatusCode)
                {
                    _loggedInUser.ResetUser();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private void SetAuthenticationHeaders()
        {
            _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
            _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_loggedInUser.Token}");
        }
    }
}
