using Caliburn.Micro;

namespace Nt.Utils.ControlInterfaces
{
    public class NtViewModelBase : Screen
    {
        private NtControlBase control;

        public NtControlBase Control
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
            NotifyOfPropertyChange(nameof(e.PropertyName));
        }
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
