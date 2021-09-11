using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.EventModels;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Models;
using RegistrationSample.OldDesktopUI.Utility;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class ShellViewModel : ObservableObject, ISubscriber<NavigationEvent>
    {
        private IViewModel _currentViewModel;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IServiceProvider _service;

        public ShellViewModel(UserDisplayModel logedInUser, IEventAggregator eventAggregator, IUserEndpoint userEndpoint, IServiceProvider service)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubsribeEvent(this);
            _userEndpoint = userEndpoint;
            _service = service;
            User = logedInUser;
            GoToLogInCmd = new RelayCommand(Navigate<LoginViewModel>);
            GoToRegistrationCmd = new RelayCommand(Navigate<RegistrationViewModel>);
            LogOutCmd = new AsyncRelayCommand(LogOut);
            Navigate<EntryViewModel>();
        }

        public ICommand GoToLogInCmd { get; }
        public ICommand GoToRegistrationCmd { get; }
        public ICommand LogOutCmd { get; }
        public UserDisplayModel User { get; }
        public bool IsUserLoggedIn => !(User?.Token is null);
        public IViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public void OnEventHandler(NavigationEvent e)
        {
            Navigate(e.ViewModelType);
        }
        private async Task LogOut()
        {
            await _userEndpoint.LogUserOut();
            User.ResetUser();
            Navigate<EntryViewModel>();
        }

        private void Navigate<T>() where T : IViewModel
        {
            Navigate(typeof(T));
        }
        private void Navigate(Type viewModelType)
        {
            if (!typeof(IViewModel).IsAssignableFrom(viewModelType))
            {
                throw new ArgumentException("Provided type is not a ViewModel", nameof(viewModelType));
            }

            if (viewModelType == typeof(LoginViewModel))
            {
                CurrentViewModel = _service.GetService<LoginViewModel>();
            }
            else if (viewModelType == typeof(UserViewModel))
            {
                CurrentViewModel = _service.GetService<UserViewModel>();
            }
            else if (viewModelType == typeof(RegistrationViewModel))
            {
                CurrentViewModel = _service.GetService<RegistrationViewModel>();
            }
            else if (viewModelType == typeof(EntryViewModel))
            {
                CurrentViewModel = _service.GetService<EntryViewModel>();
            }
            else
            {
                throw new ArgumentException("Provided View does not exists");
            }
        }

    }
}