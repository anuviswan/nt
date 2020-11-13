﻿using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using Nt.Controls.Navbar;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.ExtensionMethods;
using Nt.Utils.Helper;
using Nt.Utils.Messages;
using Nt.Utils.ServiceInterfaces;
using System.Threading.Tasks;
using System.Windows;

namespace Nt.WpfClient.ViewModels
{
    public class ShellViewModel:Conductor<object>, IHandle<UserLoggedInMessage>
    {
        private ICurrentUserService _currentUserService;
        private readonly IDialogCoordinator _dialogCordinator;
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(IDialogCoordinator dialogCoordinator,ICurrentUserService currentUserService, IEventAggregator eventAggregator)
        {
            (_dialogCordinator,_currentUserService, _eventAggregator) = (dialogCoordinator,currentUserService,eventAggregator);
            _eventAggregator.Subscribe(this);
        }

        public NtViewModelBase Navbar { get; set; }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            InvokeLogin();
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

            _eventAggregator.PublishOnUIThread(new UserLoggedInMessage(this));
            
        }

        public void Handle(UserLoggedInMessage message)
        {
            Navbar = IoC.Get<NavbarControl>().ViewModel;
        }

        public void MenuSelectionChanged(object h, object eventArgs)
        {

        }

        public void MenuSelectionChanged()
        {

        }
    }
}
