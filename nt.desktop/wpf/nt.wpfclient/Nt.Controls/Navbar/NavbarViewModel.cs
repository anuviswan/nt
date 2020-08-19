using Nt.Utils.ControlInterfaces;

namespace Nt.Controls.Navbar
{
    public class NavbarViewModel:NtViewModelBase<NavbarControl>
    {
        public string UserName { get; set; }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            TypedControl.LoadUserDetails();
        }
    }
}
