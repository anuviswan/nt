using System;
using System.Collections.Generic;
using System.Text;
using Nt.Shared.Utils.Helpers;

namespace Nt.Shared.Utils.ControlBase
{
    public class ViewModelBase:PropertyChangedBase
    {
        private ControlBase control;

        public ControlBase Control
        {
            get => control;
            set
            {
                control = value;
                control.PropertyChanged += ControlPropertyChanged;
            }
        }

        protected virtual void ControlPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyOnPropertyChanged(nameof(e.PropertyName));
        }
    }
    public class ViewModelBase<TControl>:ViewModelBase where TControl : ControlBase
    {
        public TControl TypedControl => (TControl)Control;
    }
}
