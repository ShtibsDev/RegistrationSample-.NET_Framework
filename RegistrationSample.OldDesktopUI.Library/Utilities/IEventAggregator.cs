using RegistrationSample.OldDesktopUI.Library.EventModels;

namespace RegistrationSample.OldDesktopUI.Library.Utilities
{
    public interface IEventAggregator
    {
        void PublishEvent<TEventType>(TEventType eventToPublish) where TEventType : IEventModel;
        void SubsribeEvent(object subscriber);
    }
}