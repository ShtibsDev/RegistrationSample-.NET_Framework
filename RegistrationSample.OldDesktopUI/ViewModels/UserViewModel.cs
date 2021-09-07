using RegistrationSample.OldDesktopUI.Library.Models;
using RegistrationSample.OldDesktopUI.Library.Utilities;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel(ILoggedInUserModel loggedInUser, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            User = loggedInUser;
        }
        public ILoggedInUserModel User { get; }
    }
}
