using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class LoginViewModel : ObservableObject, IViewModel
    {
        private readonly IApiHelper _api;
        private ILoggedInUserModel _loggedInUser;

        private string _username;
        private string _password;

        public LoginViewModel(IApiHelper api, ILoggedInUserModel logedInUser)
        {
            _api = api;
            _loggedInUser = logedInUser;
            LogInCmd = new AsyncRelayCommand(LogIn);
            (LogInCmd as AsyncRelayCommand).PropertyChanged += LogInCmd_CanExecuteChanged;
        }

        private void LogInCmd_CanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AsyncRelayCommand.IsRunning))
            {
                OnPropertyChanged(nameof(CanLogIn));
            }
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
        public ILoggedInUserModel LogedInUser
        {
            get => _loggedInUser;
            set => SetProperty(ref _loggedInUser, value);
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

        public string Name { get; set; }

        private async Task LogIn()
        {
            var result = await _api.Authenticate(Username, Password);

            await _api.GetLogedInUserInfo();

            MessageBox.Show($"Hello {_loggedInUser.FirstName} {_loggedInUser.LastName}!");
        }
    }
}