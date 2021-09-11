using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RegistrationSample.OldDesktopUI.Models
{
    public class UserDisplayModel : ObservableObject
    {
        private string _firstName;
        private string _lastName;
        private string _emailAddress;
        private DateTime _birthDate;
        private string _token;

        public string Id { get; set; }
        public string Token
        {
            get => _token;
            set
            {
                SetProperty(ref _token, value);
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }
        [Required]
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        [Required]
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
        public DateTime LastLogin { get; set; }
        public bool IsLoggedIn => !(Token is null);

        public void AssignUser(UserDisplayModel user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BirthDate = user.BirthDate;
            EmailAddress = user.EmailAddress;
            LastLogin = user.LastLogin.ToLocalTime();
            Token = user.Token;
        }
        public void ResetUser()
        {
            foreach (var property in GetType().GetProperties())
            {
                if(property.SetMethod != null)
                {
                    property.SetValue(this, default);
                }
            }
        }
        public UserDisplayModel Clone()
        {
            var user = new UserDisplayModel();
            user.AssignUser(this);
            return user;
        }
    }
}
