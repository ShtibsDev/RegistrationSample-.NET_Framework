using System.ComponentModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class ShellViewModel : ObservableObject, IViewModel
    {
        private IViewModel _currentView;
        private ILoggedInUserModel _loggedInUser;

        public ShellViewModel(LoginViewModel loginViewModel, ILoggedInUserModel logedInUser)
        {
            _currentView = loginViewModel;
            _loggedInUser = logedInUser;
            _loggedInUser.PropertyChanged += LoggedInUserCanaged;
        }

        private void LoggedInUserCanaged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsUserLoggedIn));
        }

        public IViewModel CurrentViewModel
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }
        public ILoggedInUserModel LogedInUser
        {
            get => _loggedInUser;
            set => SetProperty(ref _loggedInUser, value);
        }
        public bool IsUserLoggedIn
        {
            get => !string.IsNullOrEmpty(LogedInUser.Id);
        }

        public string Name { get; set; }
    }
}
