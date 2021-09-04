using System;
using System.ComponentModel;

namespace RegistrationSample.OldDesktopUI.Library.Models
{
    public interface ILoggedInUserModel : INotifyPropertyChanged
    {
        DateTime BirthDate { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        DateTime LastLogin { get; set; }
        string LastName { get; set; }
        string Token { get; set; }
    }
}