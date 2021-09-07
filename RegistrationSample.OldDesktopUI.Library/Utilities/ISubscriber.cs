using RegistrationSample.OldDesktopUI.Library.EventModels;

namespace RegistrationSample.OldDesktopUI.Library.Utilities
{
    public interface ISubscriber<T> where T : IEventModel
    {
        void OnEventHandler(T e);
    }
}
