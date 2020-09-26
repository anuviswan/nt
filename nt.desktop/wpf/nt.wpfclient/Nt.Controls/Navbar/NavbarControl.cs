﻿using Nt.Utils.ControlInterfaces;
using Nt.Utils.ServiceInterfaces;
using System.Security.Policy;

namespace Nt.Controls.Navbar
{
    public class NavbarControl : NtControlBase<NavbarViewModel>
    {
        private ICurrentUserService _currentUserService;
        public NavbarControl(ICurrentUserService currentUserService):base()
        {
            _currentUserService = currentUserService;
        }

        public void LoadUserDetails()
        {
            ViewModel.UserName = _currentUserService.DisplayName;
        }
    }
}