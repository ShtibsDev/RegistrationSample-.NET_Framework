using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using RegistrationSample.DesktopUI.Library.Models;

namespace RegistrationSample.DesktopUI.Library.API
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
                    var result = await JsonSerializer.DeserializeAsync<AuthenticatedUser>(await response.Content.ReadAsStreamAsync());
                    _loggedInUser.Token = result.access_token;
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
                    var result = await JsonSerializer.DeserializeAsync<LogedInUserModel>(await response.Content.ReadAsStreamAsync());
                    _loggedInUser.AssignUser(result, true);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task Update()
        {
            using (var response = await _apiHelper.ApiClient.PutAsJsonAsync("api/User/", _loggedInUser))
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
