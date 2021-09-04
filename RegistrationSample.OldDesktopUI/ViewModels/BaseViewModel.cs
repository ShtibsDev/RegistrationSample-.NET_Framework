using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    class BaseViewModel : ObservableObject, IViewModel
    {
        public string Name { get; set; }

        protected void NotifyChange(string propertyName)
        {
        }
    }
}
