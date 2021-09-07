using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.Library.EventModels;
using RegistrationSample.OldDesktopUI.Library.Utilities;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class EntryViewModel : BaseViewModel
    {
        public EntryViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            GoToLogInCmd = new RelayCommand(Navigate<LoginViewModel>);
        }

        public ICommand GoToLogInCmd { get; }
        public ICommand GoToRegistrationCmd { get; }
    }
}