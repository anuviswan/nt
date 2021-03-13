using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Nt.Controls.Pages.Login;
using Nt.Shared.Utils.Helpers.Commands;
using Nt.Shared.Utils.Services;

namespace Nt.Desktop.ViewModels
{
    public class ShellViewModel
    {
        public ICommand ViewLoaded { get; set; }
        public ShellViewModel()
        {
            ViewLoaded = new DelegateCommand((o)=>OnViewLoaded());
        }
       
        public void OnViewLoaded()
        {
            var windowService = new WindowService();
            windowService.ShowDialog(new LoginViewModel());
        }
    }
}
