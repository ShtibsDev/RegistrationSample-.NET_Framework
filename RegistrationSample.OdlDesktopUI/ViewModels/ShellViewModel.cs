using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class ShellViewModel : ObservableObject, IViewModel
    {
        private IViewModel _currentView;

        public ShellViewModel(LoginViewModel loginViewModel)
        {
            _currentView = loginViewModel;

        }

        public IViewModel CurrentViewModel
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }


        public string Name { get; set; }
    }
}
