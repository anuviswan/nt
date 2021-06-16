using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Nt.Shared.Utils.ControlBase;
using Nt.Shared.Utils.EventMessages;
using Nt.Shared.Utils.Helpers;
using Nt.Shared.Utils.Helpers.Commands;
using Nt.Shared.Utils.Helpers.Extensions;
using Nt.Shared.Utils.Interfaces;
using Nt.Shared.Utils.ServiceInterfaces;
using Unity;

namespace Nt.Desktop.ViewModels
{
    public class ShellViewModel:ViewModelBase, IHandle<UserLoggedInMessage>
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IWindowService _windowService;
        private readonly IUserService _currentUserService;
        private readonly IEventAggregator _eventAggregator;
        public ICommand ViewLoaded { get; set; }

        public ShellViewModel()
        {
            
        }

        public bool IsBusy { get; set; }

        [InjectionConstructor]
        public ShellViewModel(IWindowService windowService, IDialogCoordinator dialogCoordinator,IUserService userService,IEventAggregator eventAggregator)
        {
            (_dialogCoordinator,_currentUserService) = (dialogCoordinator,userService);
            (_windowService, _eventAggregator) = (windowService, eventAggregator);
            _eventAggregator.Subscribe(this);

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

            _eventAggregator.PublishMessage(new UserLoggedInMessage("User has logged in"));
           
        }

        public IEnumerable<MenuItemViewModelBase> MenuItems { get; private set; } = Enumerable.Empty<MenuItemViewModelBase>();

        private IEnumerable<MenuItemViewModelBase> InitializeMenuItems()
        {
            yield return new UserProfileViewModel();
        }

        public Task HandleAsync(UserLoggedInMessage message)
        {
            MenuItems = InitializeMenuItems().ToList();
            NotifyOnPropertyChanged(nameof(MenuItems));
            return Task.CompletedTask;
        }
    }
}
