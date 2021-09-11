using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.DesktopUI.Library.Utilities;

namespace RegistrationSample.DesktopUI.ViewModels
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