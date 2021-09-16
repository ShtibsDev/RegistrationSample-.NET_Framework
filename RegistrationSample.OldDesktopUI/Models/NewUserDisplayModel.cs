using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RegistrationSample.OldDesktopUI.Models
{
    public class NewUserDisplayModel : ObservableValidator
    {
        private string _emailAddress;
        private string _password;
        private string _confirmPassword;
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate = DateTime.Now;

        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string EmailAddress
        {
            get => _emailAddress;
            set => SetProperty(ref _emailAddress, value, true);
        }
        [Required]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, true);
        }
        [Compare(nameof(Password))]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value, true);
        }
        [Required]
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value, true);
        }
        [Required]
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value, true);
        }
        [Required]
        public DateTime BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value, true);
        }

        public void Validate()
        {
            ValidateAllProperties();
        }
    }
}