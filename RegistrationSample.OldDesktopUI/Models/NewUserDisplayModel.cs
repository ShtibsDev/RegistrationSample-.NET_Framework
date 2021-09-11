using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RegistrationSample.OldDesktopUI.Models
{
    public class NewUserDisplayModel : ObservableObject
    {
        private string _emailAddress;
        private string _password;
        private string _confirmPassword;
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;

        [Required]
        [EmailAddress]
        public string EmailAddress
        {
            get => _emailAddress;
            set => SetProperty(ref _emailAddress, value);
        }
        [Required]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        [Compare(nameof(Password))]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
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
        [Required]
        public DateTime BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }
    }
}
