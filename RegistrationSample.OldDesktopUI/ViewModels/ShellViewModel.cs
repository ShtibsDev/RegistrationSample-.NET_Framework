using System.Linq;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.EventModels;
using RegistrationSample.OldDesktopUI.Library.Models;
using RegistrationSample.OldDesktopUI.Utility;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class ShellViewModel : ObservableObject, ISubscriber<UserLoggedInEvent>
    {
        private ILoggedInUserModel _loggedInUser;
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(ILoggedInUserModel logedInUser, IEventAggregator eventAggregator, INavigation navigation)
        {
            _loggedInUser = logedInUser;
            _eventAggregator = eventAggregator;
            _eventAggregator.SubsribeEvent(this);
            Navigation = navigation;
            _eventAggregator.PublishEvent(new NavigationEvent(typeof(EntryViewModel)));
            GoToLogInCmd = new RelayCommand(() => _eventAggregator.PublishEvent(new NavigationEvent(typeof(LoginViewModel))));
            LogOutCmd = new RelayCommand(() => _eventAggregator.PublishEvent(new NavigationEvent(typeof(EntryViewModel))));
        }

        public void OnEventHandler(UserLoggedInEvent e)
        {
            OnPropertyChanged(nameof(IsUserLoggedIn));
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
    }
}
