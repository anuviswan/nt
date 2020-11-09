using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using Nt.Controls.Navbar;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.ExtensionMethods;
using Nt.Utils.Helper;
using Nt.Utils.ServiceInterfaces;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using Unity.Injection;

namespace Nt.WpfClient.ViewModels
{
    public class ShellViewModel:Conductor<object>
    {
        private ICurrentUserService _currentUserService;
        private readonly IDialogCoordinator _dialogCordinator;


        public ShellViewModel(IDialogCoordinator dialogCoordinator,ICurrentUserService currentUserService)
        {
            (_dialogCordinator,_currentUserService) = (dialogCoordinator,currentUserService);
        }

        public NtViewModelBase Navbar { get; set; }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            InvokeLogin();
            //_currentUserService = IoC.Get<ICurrentUserService>();
            //if (!_currentUserService.IsAuthenticated)
            //{ 
            //    Application.Current.Shutdown(); 
            //}

            Navbar = IoC.Get<NavbarControl>().ViewModel;
        }
        
        private async Task InvokeLogin()
        {
            var isLoggedIn = false;
            do
            {
                var loginData = await _dialogCordinator.ShowNtLogin(this);
                if(loginData == null)
                {
                    // User has cancelled Login, Should exist.
                    Application.Current.Shutdown();
                }

                var errorMsg = new NtRef<string>();
                isLoggedIn = await _currentUserService.Authenticate(loginData.Username, loginData.Password, errorMsg);

                if (!isLoggedIn)
                {
                    await _dialogCordinator.ShowNtOkDialog(this, "Authentication Failed", errorMsg);
                }
            }
            while (!isLoggedIn);
            

            //var dialogResult = await _dialogCordinator.ShowLoginAsync(this, "Login", "lll");

            //var windowManager = IoC.Get<IWindowManager>();
            //var loginControl = IoC.Get<LoginControl>();
            //windowManager.ShowNtDialog(loginControl.ViewModel,NtWindowSize.SmallLandscape); 
        }

    }
}
