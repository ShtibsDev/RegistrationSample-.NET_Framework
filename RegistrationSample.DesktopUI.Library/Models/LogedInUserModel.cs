using Microsoft.Toolkit.Mvvm.ComponentModel;
using RegistrationSample.DesktopUI.Library.EventModels;
using RegistrationSample.DesktopUI.Library.Utilities;

namespace RegistrationSample.DesktopUI.Library.Models
{
    public class LogedInUserModel : ObservableObject, ILoggedInUserModel
    {
        private string _id;
        private string _token;
        private string _firstName;
        private string _lastName;
        private string _emailAddress;
        private DateTime _birthDate;
        private DateTime _lastLogin;
        private readonly IEventAggregator _eventAggregator;

        public LogedInUserModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public string Token
        {
            get => _token;
            set => SetProperty(ref _token, value);
        }
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string EmailAddress
        {
            get => _emailAddress;
            set => SetProperty(ref _emailAddress, value);
        }
        public DateTime BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }
        public DateTime LastLogin
        {
            get => _lastLogin;
            set => SetProperty(ref _lastLogin, value);
        }

        public void AssignUser(ILoggedInUserModel user, bool broadcast = false)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BirthDate = user.BirthDate;
            EmailAddress = user.EmailAddress;
            LastLogin = user.LastLogin;

            if (broadcast)
            {
                BroadcastChange();
            }
        }
        public void ResetUser()
        {
            foreach (var property in GetType().GetProperties())
            {
                property.SetValue(this, default);
            }
            BroadcastChange();
        }
        private void BroadcastChange()
        {
            _eventAggregator.PublishEvent(new UserChangedEvent());
        }

        public ILoggedInUserModel Clone()
        {
            var user = new LogedInUserModel(_eventAggregator);
            user.AssignUser(this);
            return user;
        }
    }
}