using System.Collections.Generic;
using System.Windows.Input;
using RegistrationSample.OldDesktopUI.Library.Models;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public interface IShellViewModel
    {
        ICommand LogOutCmd { get; }
        ICommand GoToLogInCmd { get; }
        bool IsUserLoggedIn { get; }
        IEnumerable<IViewModel> ViewModels {  get; }
        IViewModel CurrentViewModel { get; set; }
        ILoggedInUserModel LogedInUser { get;}
    }
}