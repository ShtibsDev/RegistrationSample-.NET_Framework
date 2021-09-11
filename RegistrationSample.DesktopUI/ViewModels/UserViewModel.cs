using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.DesktopUI.Library.API;
using RegistrationSample.DesktopUI.Library.EventModels;
using RegistrationSample.DesktopUI.Library.Models;
using RegistrationSample.DesktopUI.Library.Utilities;

namespace RegistrationSample.DesktopUI.ViewModels
{
    public class UserViewModel : BaseViewModel, ISubscriber<UserChangedEvent>
    {
        private bool _isInEditMode;
        private ILoggedInUserModel _userUpdate;
        private readonly IUserEndpoint _userEndpoint;

        public UserViewModel(ILoggedInUserModel loggedInUser, IUserEndpoint userEndpoint, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _userEndpoint = userEndpoint;
            User = loggedInUser;
            _eventAggregator.SubsribeEvent(this);
            ActivateEditModeCmd = new RelayCommand(() => IsInEditMode = true);
            SaveChangesCmd = new AsyncRelayCommand(SaveChanges, CanSaveChanges);
            DiscarChangesCmd = new RelayCommand(DiscardChanges);

        }
        public ICommand ActivateEditModeCmd { get; }
        public ICommand SaveChangesCmd { get; }
        public ICommand DiscarChangesCmd { get; }
        public ILoggedInUserModel User { get; }
        public ILoggedInUserModel UserUpdate
        {
            get => _userUpdate;
            set => SetProperty(ref _userUpdate, value);
        }
        public bool IsInEditMode
        {
            get => _isInEditMode;
            set => SetProperty(ref _isInEditMode, value);
        }

        private bool CanSaveChanges()
        {
            return !(SaveChangesCmd as AsyncRelayCommand).IsRunning;
        }
        private async Task SaveChanges()
        {
            User.AssignUser(UserUpdate);
            await _userEndpoint.Update();
            IsInEditMode = false;
        }
        private void DiscardChanges()
        {
            var result = MessageBox.Show("Are you sure you want to discard the changes you have made?", null, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                IsInEditMode = false;
                UserUpdate = User.Clone();
            }
        }

        public void OnEventHandler(UserChangedEvent e)
        {
            UserUpdate = User.Clone();
        }
    }
}