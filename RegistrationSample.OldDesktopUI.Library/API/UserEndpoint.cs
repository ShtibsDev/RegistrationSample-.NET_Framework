using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.Library.API
{
    public class UserEndpoint : IUserEndpoint
    {
        private readonly IApiHelper _apiHelper;
        private readonly AuthenticatedUser _authenticatedUser;

        public UserEndpoint(IApiHelper apiHelper, AuthenticatedUser authenticatedUser)
        {
            _apiHelper = apiHelper;
            _authenticatedUser = authenticatedUser;
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
                    _authenticatedUser.Access_token = result.Access_token;
                    SetAuthenticationHeaders();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<LoggedInUserModel> GetLogedInUserInfo()
        {
            using (var response = await _apiHelper.ApiClient.GetAsync("api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<LoggedInUserModel> RegisterUser(NewUserModel newUser)
        {
            var regestrationUser = new { Email = newUser.EmailAddress, newUser.Password, newUser.ConfirmPassword };

            using (var response = await _apiHelper.ApiClient.PostAsJsonAsync(" api/Account/Register", regestrationUser))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

            var authenticatedUser = await Authenticate(newUser.EmailAddress, newUser.Password);

            if (authenticatedUser is null)
            {
                throw new Exception("Authentication Faild");
            }

            var userDetails = new LoggedInUserModel
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                EmailAddress = newUser.EmailAddress,
                BirthDate = newUser.BirthDate
            };

            using (var response = await _apiHelper.ApiClient.PostAsJsonAsync(" api/Account/Register", userDetails))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await GetLogedInUserInfo();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task Update(LoggedInUserModel user)
        {
            using (var response = await _apiHelper.ApiClient.PutAsJsonAsync("api/User/", user))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task LogUserOut()
        {
            using (var response = await _apiHelper.ApiClient.PostAsync("api/Account/Logout", null))
            {
                if (!response.IsSuccessStatusCode)
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
            _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authenticatedUser.Access_token}");
        }
    }
}
