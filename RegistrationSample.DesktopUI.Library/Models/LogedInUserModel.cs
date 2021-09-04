namespace RegistrationSample.DesktopUI.Library.Models
{
    public class LogedInUserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
