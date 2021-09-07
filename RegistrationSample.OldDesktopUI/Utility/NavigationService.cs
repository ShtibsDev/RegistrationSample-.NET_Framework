using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using RegistrationSample.OldDesktopUI.Library.EventModels;
using RegistrationSample.OldDesktopUI.Library.Utilities;
using RegistrationSample.OldDesktopUI.ViewModels;

namespace RegistrationSample.OldDesktopUI.Utility
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly IShellViewModel _shellViewModel;
        private readonly IEventAggregator _eventAggregator;

        public NavigationService(IShellViewModel shellViewModel, IEventAggregator eventAggregator)
        {
            _shellViewModel = shellViewModel;
            _eventAggregator = eventAggregator;
            //_eventAggregator.SubsribeEvent(this);
        }

        public void Navigate<T>() where T : IViewModel
        {
            _shellViewModel.CurrentViewModel = _shellViewModel.ViewModels.First(vm => vm is T);
            _eventAggregator.PublishEvent(new NavigationEvent(typeof(T)));
        }
        public void OnEventHandler(NavigationEvent e)
        {

        }
    }
}
