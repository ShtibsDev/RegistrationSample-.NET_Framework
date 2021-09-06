using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class UserViewModel : ObservableObject, IViewModel
    {
        public UserViewModel(ILoggedInUserModel loggedInUser)
        {
            User = loggedInUser;
        }
        public ILoggedInUserModel User{ get; }
        public string Name { get; set; }
    }
}
