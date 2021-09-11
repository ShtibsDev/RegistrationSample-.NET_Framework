using Microsoft.Toolkit.Mvvm.ComponentModel;
using RegistrationSample.DesktopUI.Library.EventModels;
using RegistrationSample.DesktopUI.Library.Utilities;

namespace RegistrationSample.DesktopUI.ViewModels
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
