using System.Collections.Generic;
using RegistrationSample.OldDesktopUI.ViewModels;

namespace RegistrationSample.OldDesktopUI.Utility
{
    public interface INavigationService
    {
        void Navigate<T>() where T : IViewModel;
    }
}