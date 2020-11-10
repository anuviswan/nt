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
        public NtViewModelBase()
        {
            TypedControl.PropertyChanged += ControlPropertyChanged;
        }

        private void ControlPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyOfPropertyChange(nameof(e.PropertyName));
        }

        public TControl TypedControl 
        {
            get => (TControl)Control;
            set => Control = value;
        }
    }
}
