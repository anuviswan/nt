using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Utils.ControlInterfaces
{
    public abstract class NtControlBase
    {
        public abstract NtViewModelBase ViewModel { get; set; }
    }
}
