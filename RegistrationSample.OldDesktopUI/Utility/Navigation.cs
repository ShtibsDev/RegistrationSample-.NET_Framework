using System.Collections.Generic;
using System.Linq;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RegistrationSample.OldDesktopUI.Library.EventModels;
using RegistrationSample.OldDesktopUI.Library.Utilities;
using RegistrationSample.OldDesktopUI.ViewModels;

namespace RegistrationSample.OldDesktopUI.Utility
{
    public class Navigation : ObservableObject, INavigation, ISubscriber<NavigationEvent>
    {
        private IViewModel _currentViewModel;
        private IEventAggregator _eventAggregator;

        public Navigation(IEnumerable<IViewModel> viewModels, IEventAggregator eventAggregator)
        {
            ViewModels = viewModels;
            _eventAggregator = eventAggregator;
            _eventAggregator.SubsribeEvent(this);
        }

        public IEnumerable<IViewModel> ViewModels { get; }
        public IViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public void Navigate<T>() where T : IViewModel
        {
            _eventAggregator.PublishEvent(new NavigationEvent(typeof(T)));
        }
        public void OnEventHandler(NavigationEvent e)
        {
            CurrentViewModel = ViewModels.FirstOrDefault(vm => vm.GetType() == e.ViewModelType);
        }
    }
}
