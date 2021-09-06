using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RegistrationSample.OldDesktopUI.EventModels;
using RegistrationSample.OldDesktopUI.Utility;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class EntryViewModel : ObservableObject, IViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        public EntryViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            GoToLogInCmd = new RelayCommand(() => _eventAggregator.PublishEvent(new NavigationEvent(typeof(LoginViewModel))));
        }

        public ICommand GoToLogInCmd { get; }
        public ICommand GoToRegistrationCmd { get; }
        public string Name { get; set; }

    }
}