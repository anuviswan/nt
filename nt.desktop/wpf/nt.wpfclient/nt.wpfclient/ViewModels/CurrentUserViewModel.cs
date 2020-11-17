using Caliburn.Micro;
using MahApps.Metro.IconPacks;
using Nt.Controls.UserProfile;
using Nt.Controls.EditUserProfile;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.ServiceInterfaces;
using Nt.WpfClient.ViewModels.Base;
using Nt.Utils.ExtensionMethods;

namespace Nt.WpfClient.ViewModels
{
    public class CurrentUserViewModel : NtMenuItemViewModelBase
    {
        public override string Title => "User";
        public override object Icon => new PackIconMaterial { Kind = PackIconMaterialKind.AccountStar };

        private ICurrentUserService _currentUserService;
        public CurrentUserViewModel(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            var user = IoC.Get<UserProfileControl>();
            user.UserDisplayName = _currentUserService.DisplayName;
            user.UserName = _currentUserService.UserName;
            user.Bio = _currentUserService.Bio;

            CurrentUser = user.ViewModel;
            NotifyOfPropertyChange(nameof(CurrentUser));
        }

        public NtViewModelBase CurrentUser { get; set; }

        public void EditUser()
        {
            var editUser = IoC.Get<EditUserProfileControl>();
            editUser.UserName = _currentUserService.UserName;
            editUser.UserDisplayName = _currentUserService.DisplayName;
            editUser.Bio = _currentUserService.Bio;

            var windowManager = IoC.Get<IWindowManager>();
            windowManager.ShowNtDialog(this,editUser.ViewModel, NtWindowSize.LargeLandscape);
        }

    }
}