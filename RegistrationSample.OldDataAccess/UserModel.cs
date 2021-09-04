using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationSample.OdlDataAccess.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
