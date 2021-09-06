using System.Collections.Generic;
using RegistrationSample.OldDesktopUI.ViewModels;

namespace RegistrationSample.OldDesktopUI.Utility
{
    public interface INavigation
    {
        IViewModel CurrentViewModel { get; set; }
        IEnumerable<IViewModel> ViewModels { get; }
    }
}