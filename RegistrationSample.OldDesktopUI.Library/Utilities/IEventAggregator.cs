namespace RegistrationSample.OldDesktopUI.Library.Utilities
{
    public interface IEventAggregator
    {
        void PublishEvent<TEventType>(TEventType eventToPublish);
        void SubsribeEvent(object subscriber);
    }
}