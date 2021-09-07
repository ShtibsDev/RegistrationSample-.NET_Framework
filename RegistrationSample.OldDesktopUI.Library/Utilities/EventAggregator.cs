using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RegistrationSample.OldDesktopUI.Library.EventModels;

namespace RegistrationSample.OldDesktopUI.Library.Utilities
{
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, List<WeakReference>> _eventSubsribers = new Dictionary<Type, List<WeakReference>>();
        private readonly object _lockSubscriberDictionary = new object();

        public void PublishEvent<T>(T eventToPublish) where T : IEventModel
        {
            var subsriberType = typeof(ISubscriber<>).MakeGenericType(typeof(T));

            var subscribers = GetSubscriberList(subsriberType);

            var subsribersToBeRemoved = new List<WeakReference>();

            foreach (var weakSubsriber in subscribers)
            {
                if (weakSubsriber.IsAlive)
                {
                    var subscriber = (ISubscriber<T>)weakSubsriber.Target;

                    InvokeSubscriberEvent(eventToPublish, subscriber);
                }
                else
                {
                    subsribersToBeRemoved.Add(weakSubsriber);
                }
            }


            if (subsribersToBeRemoved.Any())
            {
                lock (_lockSubscriberDictionary)
                {
                    foreach (var remove in subsribersToBeRemoved)
                    {
                        subscribers.Remove(remove);
                    }
                }
            }
        }

        public void SubsribeEvent(object subscriber)
        {
            lock (_lockSubscriberDictionary)
            {
                var subsriberTypes = subscriber.GetType().GetInterfaces()
                                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

                var weakReference = new WeakReference(subscriber);

                foreach (var subsriberType in subsriberTypes)
                {
                    var subscribers = GetSubscriberList(subsriberType);

                    subscribers.Add(weakReference);
                }
            }
        }

        private void InvokeSubscriberEvent<T>(T eventToPublish, ISubscriber<T> subscriber) where T : IEventModel
        {
            var syncContext = SynchronizationContext.Current;

            if (syncContext == null)
            {
                syncContext = new SynchronizationContext();
            }

            syncContext.Post(s => subscriber.OnEventHandler(eventToPublish), null);
        }

        private List<WeakReference> GetSubscriberList(Type subsriberType)
        {
            List<WeakReference> subsribersList = null;

            lock (_lockSubscriberDictionary)
            {
                var found = _eventSubsribers.TryGetValue(subsriberType, out subsribersList);

                if (!found)
                {
                    subsribersList = new List<WeakReference>();

                    _eventSubsribers.Add(subsriberType, subsribersList);
                }
            }

            return subsribersList;
        }
    }
}
