using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Library.EventModels;
using RegistrationSample.OldDesktopUI.Library.Models;
using RegistrationSample.OldDesktopUI.Library.Utilities;
using RegistrationSample.OldDesktopUI.Utility;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class ShellViewModel : ObservableObject, ISubscriber<UserChangedEvent>
    {
        private ILoggedInUserModel _loggedInUser;
        private readonly IEventAggregator _eventAggregator;
        private readonly IApiHelper _api;
        public ShellViewModel(ILoggedInUserModel logedInUser, IEventAggregator eventAggregator, INavigation navigation, IApiHelper api)
        {
            _loggedInUser = logedInUser;
            _eventAggregator = eventAggregator;
            _eventAggregator.SubsribeEvent(this);
            Navigation = navigation;
            _api = api;
            _eventAggregator.PublishEvent(new NavigationEvent(typeof(EntryViewModel)));
            GoToLogInCmd = new RelayCommand(() => Navigation.Navigate<LoginViewModel>());
            LogOutCmd = new AsyncRelayCommand(LogOut);
        }

        public ICommand GoToLogInCmd { get; }
        public ICommand LogOutCmd { get; }
        public INavigation Navigation { get; }
        public ILoggedInUserModel LogedInUser
        {
            get => _loggedInUser;
            set => SetProperty(ref _loggedInUser, value);
        }
        public bool IsUserLoggedIn => !string.IsNullOrEmpty(LogedInUser.Id);

        public void OnEventHandler(UserChangedEvent e)
        {
            OnPropertyChanged(nameof(IsUserLoggedIn));
        }
        private async Task LogOut()
        {
            await _api.LogUserOut();
            Navigation.Navigate<EntryViewModel>();
        }
    }
}
