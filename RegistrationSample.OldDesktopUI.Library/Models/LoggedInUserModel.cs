using System;

namespace RegistrationSample.OldDesktopUI.Library.Models
{
    public class LoggedInUserModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime LastLogin { get; set; }

        public void AssignUser(LoggedInUserModel user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BirthDate = user.BirthDate;
            EmailAddress = user.EmailAddress;
            LastLogin = user.LastLogin.ToLocalTime();
        }
        public LoggedInUserModel Clone()
        {
            var user = new LoggedInUserModel();
            user.AssignUser(this);
            return user;
        }
    }
}
