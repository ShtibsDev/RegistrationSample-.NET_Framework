using RegistrationSample.DesktopUI.Library.EventModels;

namespace RegistrationSample.DesktopUI.Library.Utilities
{
    public interface ISubscriber<T> where T : IEventModel
    {
        void OnEventHandler(T e);
    }
}
