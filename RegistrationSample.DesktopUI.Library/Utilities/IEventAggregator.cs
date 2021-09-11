using RegistrationSample.DesktopUI.Library.EventModels;

namespace RegistrationSample.DesktopUI.Library.Utilities
{
    public interface IEventAggregator
    {
        void PublishEvent<TEventType>(TEventType eventToPublish) where TEventType : IEventModel;
        void SubsribeEvent(object subscriber);
    }
}