using Nt.Utils.ControlInterfaces;
using System.ComponentModel;

namespace Nt.Controls.Navbar
{
    public class NavbarViewModel:NtViewModelBase<NavbarControl>
    {
        public string UserName 
        {
            get => TypedControl.UserName;
            set => TypedControl.UserName = value;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            TypedControl.LoadUserDetails();
        }

        protected override void ControlPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.ControlPropertyChanged(sender, e);
            NotifyOfPropertyChange(e.PropertyName);
        }
    }
}
