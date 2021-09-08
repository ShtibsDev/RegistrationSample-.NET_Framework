using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Library.EventModels;
using RegistrationSample.OldDesktopUI.Library.Models;
using RegistrationSample.OldDesktopUI.Library.Utilities;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class ShellViewModel : ObservableObject, ISubscriber<UserChangedEvent>, ISubscriber<NavigationEvent>, IShellViewModel
    {
        private IViewModel _currentViewModel;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IServiceProvider _service;

        public ShellViewModel(ILoggedInUserModel logedInUser,
                              IEventAggregator eventAggregator,
                              IEnumerable<IViewModel> viewModels,
                              IUserEndpoint userEndpoint,
                              IServiceProvider service)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubsribeEvent(this);
            ViewModels = viewModels;
            _userEndpoint = userEndpoint;
            _service = service;
            LogedInUser = logedInUser;
            CurrentViewModel = ViewModels.First(vm => vm is EntryViewModel);
            GoToLogInCmd = new RelayCommand(Navigate<LoginViewModel>);
            LogOutCmd = new AsyncRelayCommand(LogOut);
        }

        public ICommand GoToLogInCmd { get; }
        public ICommand LogOutCmd { get; }
        public ILoggedInUserModel LogedInUser{get;}
        public bool IsUserLoggedIn => !(LogedInUser.Token is null);
        public IViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }
        public IEnumerable<IViewModel> ViewModels { get; }

        public void OnEventHandler(UserChangedEvent e)
        {
            OnPropertyChanged(nameof(IsUserLoggedIn));
        }
        public void OnEventHandler(NavigationEvent e)
        {
            Navigate(e.ViewModelType);
        }
        private async Task LogOut()
        {
            await _userEndpoint.LogUserOut();
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
            else
            {
                CurrentViewModel = ViewModels.First(vm => vm.GetType() == viewModelType);
            }
        }
    }
}