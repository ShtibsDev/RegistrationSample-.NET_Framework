namespace RegistrationSample.OldDesktopUI.Utility
{
    public interface ISubscriber<TEventType>
    {
        void OnEventHandler(TEventType e);
    }
}
