using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Nt.Controls.Pages.Login;
using Nt.Desktop.Bootstrap;
using Nt.Shared.Utils.ControlBase;
using Nt.Shared.Utils.Helpers.Commands;
using Nt.Shared.Utils.ServiceInterfaces;
using Nt.Shared.Utils.Services;
using Unity;

namespace Nt.Desktop.ViewModels
{
    public class ShellViewModel:ViewModelBase
    {
        private  IWindowService _windowService;
        public ICommand ViewLoaded { get; set; }

        public ShellViewModel()
        {

        }

        [InjectionConstructor]
        public ShellViewModel(IWindowService windowService)
        {
            _windowService = windowService;
           
            ViewLoaded = new DelegateCommand((o)=>OnViewLoaded());
        }
       
        public void OnViewLoaded()
        {
            var loginViewModel = IoC.Get<LoginViewModel>();
            _windowService.ShowDialog(loginViewModel);
        }
    }
}
