using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Controls.Login;
using Nt.Utils.ServiceInterfaces;

namespace Nt.WpfClient.ViewModels
{
    public class ShellViewModel:Conductor<object>
    {
        public ShellViewModel()
        {
            
        }

        protected override void OnViewAttached(object view, object context)
        {
            InvokeLogin();
        }
        private void InvokeLogin()
        {
            var windowManager = IoC.Get<IExtendedWindowManager>();
            windowManager.Show(new LoginViewModel()); 
        }

      

    }
}
