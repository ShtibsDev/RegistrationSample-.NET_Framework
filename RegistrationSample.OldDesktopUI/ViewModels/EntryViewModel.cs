using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class EntryViewModel : ObservableObject, IViewModel
    {
        private ShellViewModel _shell;
        public string Name { get; set; }

        public EntryViewModel(ShellViewModel shell)
        {
            _shell = shell;

        }

        public ICommand GoToLogInCmd { get; }
        public ICommand GoToRegistrationCmd { get; }

    }
}
