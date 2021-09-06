namespace RegistrationSample.OldDesktopUI.Library.Utilities
{
    public interface ISubscriber<TEventType>
    {
        void OnEventHandler(TEventType e);
    }
}
