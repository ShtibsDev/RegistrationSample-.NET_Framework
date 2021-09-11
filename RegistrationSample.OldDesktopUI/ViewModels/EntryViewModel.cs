using System.Windows.Input;
using AutoMapper;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.Utility;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class EntryViewModel : BaseViewModel
    {
        public EntryViewModel(IEventAggregator eventAggregator) : base(eventAggregator: eventAggregator)
        {
            GoToLogInCmd = new RelayCommand(Navigate<LoginViewModel>);
            GoToRegistrationCmd = new RelayCommand(Navigate<RegistrationViewModel>);
        }

        public ICommand GoToLogInCmd { get; }
        public ICommand GoToRegistrationCmd { get; }
    }
}