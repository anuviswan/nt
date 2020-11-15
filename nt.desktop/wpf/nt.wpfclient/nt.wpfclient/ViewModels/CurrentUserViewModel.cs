using Caliburn.Micro;
using MahApps.Metro.IconPacks;
using Nt.Controls.UserProfile;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.ServiceInterfaces;
using Nt.WpfClient.ViewModels.Base;

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
            user.DisplayName = _currentUserService.DisplayName;
            user.UserName = _currentUserService.UserName;
            user.Bio = _currentUserService.Bio;

            CurrentUser = user.ViewModel;
            NotifyOfPropertyChange(nameof(CurrentUser));
        }

        public NtViewModelBase CurrentUser { get; set; }

    }
}