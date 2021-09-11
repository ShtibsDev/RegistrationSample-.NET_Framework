using RegistrationSample.OldDesktopUI.EventModels;

namespace RegistrationSample.OldDesktopUI.Utility
{
    public interface IEventAggregator
    {
        void PublishEvent<TEventType>(TEventType eventToPublish) where TEventType : IEventModel;
        void SubsribeEvent(object subscriber);
    }
}