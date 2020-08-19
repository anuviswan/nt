using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Utils.ControlInterfaces
{
    public class NtViewModelBase : Screen
    {
        public object Control { get; set; }
    }
    public class NtViewModelBase<TControl> : NtViewModelBase where TControl:NtControlBase
    {
        public TControl TypedControl 
        {
            get => (TControl)Control;
            set => Control = value;
        }
    }
}
