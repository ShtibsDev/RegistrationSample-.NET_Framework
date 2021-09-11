using System;
using System.ComponentModel.DataAnnotations;

namespace RegistrationSample.DataAccess.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "The provided email is invalid")]
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
