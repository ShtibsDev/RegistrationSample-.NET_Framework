using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Library.Utilities;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private readonly IUserEndpoint _userEndpoint;

        public LoginViewModel(IUserEndpoint userEndpoint, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _userEndpoint = userEndpoint;
            LogInCmd = new AsyncRelayCommand(LogIn);
        }

        public ICommand LogInCmd { get; }
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                OnPropertyChanged(nameof(CanLogIn));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                OnPropertyChanged(nameof(CanLogIn));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
        public bool CanLogIn
        {
            get
            {
                if (Username?.Length > 0 && Password?.Length > 0 && !(LogInCmd as AsyncRelayCommand).IsRunning)
                {
                    return true;
                }
                return false;
            }
        }

        private async Task LogIn()
        {
            try
            {
                var result = await _userEndpoint.Authenticate(Username, Password);
                await _userEndpoint.GetLogedInUserInfo();
                Navigate<UserViewModel>();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}