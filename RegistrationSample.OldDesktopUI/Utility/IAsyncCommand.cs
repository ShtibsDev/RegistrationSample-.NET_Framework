using System.Threading.Tasks;
using System.Windows.Input;

namespace RegistrationSample.OldDesktopUI.Utility
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();

        void RaiseCanExecuteChanged();
    }
}

