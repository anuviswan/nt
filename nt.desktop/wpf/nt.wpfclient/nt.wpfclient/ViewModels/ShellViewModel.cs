using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Controls.Login;

namespace Nt.WpfClient.ViewModels
{
    public class ShellViewModel:Conductor<object>
    {
        public ShellViewModel()
        {
            LoadLoginControl();
        }

        private void LoadLoginControl()
        {
            ActivateItem(new LoginViewModel());
        }

    }
}
