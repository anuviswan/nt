using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using Nt.Controls.Login;
using Nt.Controls.Navbar;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.ExtensionMethods;
using Nt.Utils.ServiceInterfaces;
using System;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;
using System.Windows;
using Unity.Injection;

namespace Nt.WpfClient.ViewModels
{
    public class ShellViewModel:Conductor<object>
    {
        private ICurrentUserService _currentUserService;
        private readonly IDialogCoordinator _dialogCordinator;


        public ShellViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCordinator = dialogCoordinator;
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
        
        private async void InvokeLogin()
        {
            var dialogResult = _dialogCordinator.ShowLoginAsync(this, "Login", "lll").Result;

            //var windowManager = IoC.Get<IWindowManager>();
            //var loginControl = IoC.Get<LoginControl>();
            //windowManager.ShowNtDialog(loginControl.ViewModel,NtWindowSize.SmallLandscape); 
        }

    }
}
