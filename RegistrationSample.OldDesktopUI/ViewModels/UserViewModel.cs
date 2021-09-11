using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.EventModels;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Library.Models;
using RegistrationSample.OldDesktopUI.Models;
using RegistrationSample.OldDesktopUI.Utility;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private bool _isInEditMode;
        private UserDisplayModel _userUpdate;
        private readonly IUserEndpoint _userEndpoint;

        public UserViewModel(UserDisplayModel currentUser, IUserEndpoint userEndpoint, IMapper mapper, IEventAggregator eventAggregator) : base(mapper, eventAggregator)
        {
            _userEndpoint = userEndpoint;
            _mapper = mapper;
            User = currentUser;
            _eventAggregator.SubsribeEvent(this);
            ActivateEditModeCmd = new RelayCommand(ActivateEditMode);
            SaveChangesCmd = new AsyncCommand(SaveChanges);
            DiscarChangesCmd = new RelayCommand(DiscardChanges);
        }

        public ICommand ActivateEditModeCmd { get; }
        public IAsyncCommand SaveChangesCmd { get; }
        public ICommand DiscarChangesCmd { get; }
        public UserDisplayModel User { get; }
        public UserDisplayModel UserUpdate
        {
            get => _userUpdate;
            set => SetProperty(ref _userUpdate, value);
        }
        public bool IsInEditMode
        {
            get => _isInEditMode;
            set => SetProperty(ref _isInEditMode, value);
        }

        private void ActivateEditMode()
        {
            IsInEditMode = true;
            UserUpdate = User.Clone();
        }
        private async Task SaveChanges()
        {
            User.AssignUser(UserUpdate);
            await _userEndpoint.Update(_mapper.Map<LoggedInUserModel>(User));
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
    }
}