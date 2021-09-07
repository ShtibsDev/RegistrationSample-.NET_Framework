using Microsoft.Toolkit.Mvvm.ComponentModel;
using RegistrationSample.OldDesktopUI.Library.EventModels;
using RegistrationSample.OldDesktopUI.Library.Utilities;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class BaseViewModel : ObservableObject, IViewModel
    {
        protected IEventAggregator _eventAggregator;

        public BaseViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        public string Name { get; set; }

        protected void Navigate<T>() where T : IViewModel
        {
            _eventAggregator.PublishEvent(new NavigationEvent(typeof(T)));
        }
    }
}
