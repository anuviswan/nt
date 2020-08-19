﻿using Caliburn.Micro;
using Nt.Controls.Login;
using Nt.Controls.Navbar;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.ExtensionMethods;
using Nt.Utils.ServiceInterfaces;
using System;
using System.Web.UI.WebControls;
using System.Windows;

namespace Nt.WpfClient.ViewModels
{
    public class ShellViewModel:Conductor<object>
    {
        private ICurrentUserService _currentUserService;

        public ShellViewModel()
        {
            
        }

        public NtViewModelBase Navbar { get; set; }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            InvokeLogin();
            _currentUserService = IoC.Get<ICurrentUserService>();
            if (!_currentUserService.IsAuthenticated)
            { 
                Application.Current.Shutdown(); 
            }

            Navbar = IoC.Get<NavbarControl>().ViewModel;
           
        }

        
        private void InvokeLogin()
        {
            var windowManager = IoC.Get<IWindowManager>();
            var loginControl = IoC.Get<LoginControl>();
            windowManager.ShowNtDialog(loginControl.ViewModel,NtWindowSize.SmallLandscape); 
        }

      

    }
}
