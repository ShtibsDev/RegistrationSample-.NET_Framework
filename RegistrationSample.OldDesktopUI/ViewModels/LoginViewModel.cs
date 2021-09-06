using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.Library.API;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class LoginViewModel : ObservableObject, IViewModel
    {
        private string _username;
        private string _password;
        private readonly IApiHelper _api;

        public LoginViewModel(IApiHelper api)
        {
            _api = api;
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
        }
    }
}