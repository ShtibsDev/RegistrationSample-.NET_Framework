using RegistrationSample.OldDesktopUI.EventModels;

namespace RegistrationSample.OldDesktopUI.Utility
{
    public interface ISubscriber<T> where T : IEventModel
    {
        void OnEventHandler(T e);
    }
}
