using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Controls.Pages.Login;
using Nt.Shared.Utils.Services;

namespace Nt.Desktop.ViewModels
{
    public class ShellViewModel
    {
        public ShellViewModel()
        {

        }
       
        public void ViewLoaded()
        {
            var windowService = new WindowService();
            windowService.ShowDialog(new LoginViewModel());
        }
    }
}
