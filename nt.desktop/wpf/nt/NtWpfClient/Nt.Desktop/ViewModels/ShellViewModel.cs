using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Nt.Controls.Pages.Login;
using Nt.Desktop.Bootstrap;
using Nt.Shared.Utils.ControlBase;
using Nt.Shared.Utils.Helpers.Commands;
using Nt.Shared.Utils.Helpers.Extensions;
using Nt.Shared.Utils.ServiceInterfaces;
using Nt.Shared.Utils.Services;
using Unity;

namespace Nt.Desktop.ViewModels
{
    public class ShellViewModel:ViewModelBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private  readonly IWindowService _windowService;
        public ICommand ViewLoaded { get; set; }

        public ShellViewModel()
        {
            
        }

        [InjectionConstructor]
        public ShellViewModel(IWindowService windowService, IDialogCoordinator dialogCoordinator)
        {
            (_windowService, _dialogCoordinator) = (windowService, dialogCoordinator);

            ViewLoaded = new DelegateCommand((_)=>OnViewLoaded());
        }
       
        public void OnViewLoaded()
        {
            var result = _dialogCoordinator.ShowNtLogin(this);

        }
    }
}
