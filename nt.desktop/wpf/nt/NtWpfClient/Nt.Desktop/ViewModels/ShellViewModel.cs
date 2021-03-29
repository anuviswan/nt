using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Nt.Shared.Utils.ControlBase;
using Nt.Shared.Utils.Helpers;
using Nt.Shared.Utils.Helpers.Commands;
using Nt.Shared.Utils.Helpers.Extensions;
using Nt.Shared.Utils.ServiceInterfaces;
using Unity;

namespace Nt.Desktop.ViewModels
{
    public class ShellViewModel:ViewModelBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IWindowService _windowService;
        private readonly IUserService _currentUserService;
        public ICommand ViewLoaded { get; set; }

        public ShellViewModel()
        {
            
        }

        public bool IsBusy { get; set; }

        [InjectionConstructor]
        public ShellViewModel(IWindowService windowService, IDialogCoordinator dialogCoordinator,IUserService userService)
        {
            (_windowService, _dialogCoordinator,_currentUserService) = (windowService, dialogCoordinator,userService);

            ViewLoaded = new DelegateCommand((_)=>Task.Run(OnViewLoaded));
        }
       
        public async Task OnViewLoaded()
        {
            var isLoggedIn = false;
            do
            {
                var loginData = await _dialogCoordinator.ShowNtLogin(this);
                if (loginData == null)
                {
                    // User has cancelled Login, Should exist.
                    Application.Current.Shutdown();
                }

                IsBusy = true;
                var errorMsg = new AsyncRef<string>();
                isLoggedIn = await _currentUserService.Authenticate(loginData.Username, loginData.Password,errorMsg);
                IsBusy = false;
                if (!isLoggedIn)
                {
                    await _dialogCoordinator.ShowNtOkDialog(this, "Authentication Failed", errorMsg);
                }

            } while (!isLoggedIn);
            

        }
    }
}
