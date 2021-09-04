using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RegistrationSample.OldDesktopUI.Library.Models
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
    }
}
