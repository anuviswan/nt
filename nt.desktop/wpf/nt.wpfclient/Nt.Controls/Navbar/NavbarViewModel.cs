using Nt.Utils.ControlInterfaces;

namespace Nt.Controls.Navbar
{
    public class NavbarViewModel:NtViewModelBase<NavbarControl>
    {
        private string _userName;
        public string UserName 
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyOfPropertyChange();
            }
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            TypedControl.LoadUserDetails();
        }
    }
}
