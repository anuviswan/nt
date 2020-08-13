using Nt.Utils.ControlInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Controls.Login
{
    public class LoginControl : NtControlBase
    {
        public override NtViewModelBase ViewModel { get; set; } = new LoginViewModel();
    }
}
