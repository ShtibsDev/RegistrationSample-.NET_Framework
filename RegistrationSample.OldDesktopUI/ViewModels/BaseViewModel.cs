using AutoMapper;
using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using RegistrationSample.OldDesktopUI.EventModels;
using RegistrationSample.OldDesktopUI.Utility;

namespace RegistrationSample.OldDesktopUI.ViewModels
{
    public class BaseViewModel : ObservableObject, IViewModel
    {
        protected IMapper _mapper;
        protected IEventAggregator _eventAggregator;

        public BaseViewModel(IMapper mapper = null, IEventAggregator eventAggregator = null)
        {
            if (mapper != null)
            {
                _mapper = mapper;
            }
            if (eventAggregator != null)
            {
                _eventAggregator = eventAggregator;
            }
        }
        public string Name { get; set; }

        protected void Navigate<T>() where T : IViewModel
        {
            if (_eventAggregator != null)
            {
                _eventAggregator.PublishEvent(new NavigationEvent(typeof(T)));
            }
            else
            {
                throw new NullReferenceException($"The constractor parameter for '${_eventAggregator}' is null.");
            }
        }
    }
}
