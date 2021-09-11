using System;

namespace RegistrationSample.OldDesktopUI.EventModels
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
