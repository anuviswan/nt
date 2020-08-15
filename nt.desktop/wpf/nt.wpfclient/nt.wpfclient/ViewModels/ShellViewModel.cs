using Caliburn.Micro;
using Nt.Controls.Login;
using Nt.Utils.ExtensionMethods;

namespace Nt.WpfClient.ViewModels
{
    public class ShellViewModel:Conductor<object>
    {
        public ShellViewModel()
        {
            
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            InvokeLogin();
        }

        
        private void InvokeLogin()
        {
            var windowManager = IoC.Get<IWindowManager>();
            windowManager.ShowNtDialog(new LoginViewModel(),NtWindowSize.SmallLandscape); 
        }

      

    }
}
