namespace RegistrationSample.OldDesktopUI.Utility
{
    public interface IEventAggregator
    {
        void PublishEvent<TEventType>(TEventType eventToPublish);
        void SubsribeEvent(object subscriber);
    }
}