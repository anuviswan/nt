using MahApps.Metro.IconPacks;
using Nt.WpfClient.ViewModels.Base;

namespace Nt.WpfClient.ViewModels
{
    public class CurrentUserViewModel : NtMenuItemViewModelBase
    {
        public override string Title => "User";
        public override object Icon => new PackIconMaterial { Kind = PackIconMaterialKind.AccountStar };
    }
}