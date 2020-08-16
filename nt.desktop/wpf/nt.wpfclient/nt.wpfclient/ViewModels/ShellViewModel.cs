using Caliburn.Micro;
using Nt.Controls.Login;
using Nt.Utils.ExtensionMethods;
using Nt.Utils.ServiceInterfaces;
using System;
using System.Windows;

namespace Nt.WpfClient.ViewModels
{
    public class ShellViewModel:Conductor<object>
    {
        private ICurrentUserService _currentUserService;

        public ShellViewModel()
        {
            
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            InvokeLogin();
            _currentUserService = IoC.Get<ICurrentUserService>();
            if (!_currentUserService.IsAuthenticated)
            { 
                Application.Current.Shutdown(); 
            }
           
        }

        
        private void InvokeLogin()
        {
            var windowManager = IoC.Get<IWindowManager>();
            windowManager.ShowNtDialog(new LoginViewModel(),NtWindowSize.SmallLandscape); 
        }

      

    }
}
