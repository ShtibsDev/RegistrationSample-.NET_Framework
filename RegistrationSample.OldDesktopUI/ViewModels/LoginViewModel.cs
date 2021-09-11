using System;
using System.Threading.Tasks;
using AutoMapper;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Models;
using RegistrationSample.OldDesktopUI.Utility;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private readonly UserDisplayModel _user;
        private readonly IUserEndpoint _userEndpoint;

        public LoginViewModel(UserDisplayModel currentUser, IUserEndpoint userEndpoint, IMapper mapper, IEventAggregator eventAggregator) : base(mapper, eventAggregator)
        {
            _user = currentUser;
            _userEndpoint = userEndpoint;
            LogInCmd = new AsyncCommand(LogIn, CanLogIn);
        }

        public IAsyncCommand LogInCmd { get; }
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                LogInCmd.RaiseCanExecuteChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                LogInCmd.RaiseCanExecuteChanged();
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool CanLogIn()
        {
            return Username?.Length > 0 && Password?.Length > 0;
        }
        private async Task LogIn()
        {
            try
            {
                var authentication = await _userEndpoint.Authenticate(Username, Password);
                var loggedInUser = await _userEndpoint.GetLogedInUserInfo();
                loggedInUser.Token = authentication.Access_token;
                _user.AssignUser(_mapper.Map<UserDisplayModel>(loggedInUser));
                Navigate<UserViewModel>();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}