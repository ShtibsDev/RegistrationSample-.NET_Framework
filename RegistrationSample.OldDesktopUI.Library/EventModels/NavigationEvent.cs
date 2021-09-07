using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSample.OldDesktopUI.Library.EventModels
{
    public class NavigationEvent : IEventModel
    {
        public Type ViewModelType { get; }

        public NavigationEvent(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }
}
