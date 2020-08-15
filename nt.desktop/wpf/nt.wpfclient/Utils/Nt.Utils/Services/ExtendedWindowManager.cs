using Caliburn.Micro;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.ServiceInterfaces;

namespace Nt.Utils.Services
{
    public class ExtendedWindowManager : IExtendedWindowManager
    {
        private IWindowManager _windowManager = IoC.Get<IWindowManager>();
        public void Show(NtViewModelBase viewModel)
        {
            _windowManager.ShowDialog(viewModel);
        }
    }
}
