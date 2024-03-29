﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.ExtensionMethods;
using Nt.Utils.Helper;
using Nt.Utils.Messages;
using Nt.Utils.ServiceInterfaces;
using Nt.WpfClient.ViewModels.Base;

namespace Nt.WpfClient.ViewModels;

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

    public bool IsBusy { get; set; }
    
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

            IsBusy = true;
            var errorMsg = new NtRef<string>();
            isLoggedIn = await _currentUserService.Authenticate(loginData.Username, loginData.Password, errorMsg);
            IsBusy = false;
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
        

        MenuItems = InitializeMenuItems();
        NotifyOfPropertyChange(nameof(MenuItems));
        // Navbar = IoC.Get<NavbarControl>().ViewModel;
    }
    public IEnumerable<NtMenuItemViewModelBase> MenuItems { get; private set; } = Enumerable.Empty<NtMenuItemViewModelBase>();
    public IEnumerable<NtMenuItemViewModelBase> InitializeMenuItems()
    {
        yield return IoC.Get<CurrentUserViewModel>();
        yield return IoC.Get<HomeViewModel>();
    }
    
}
