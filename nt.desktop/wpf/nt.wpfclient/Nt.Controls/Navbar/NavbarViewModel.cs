using Nt.Utils.ControlInterfaces;

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
    }
}
