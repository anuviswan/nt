using System;
using System.Collections.Generic;
using System.Text;
using MahApps.Metro.IconPacks;
using Nt.WpfClient.ViewModels.Base;

namespace Nt.WpfClient.ViewModels
{
    public class HomeViewModel : NtMenuItemViewModelBase
    {
        public override string Title => "Home";
        public override object Icon => new PackIconMaterial { Kind = PackIconMaterialKind.Home};

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);

        }
    }
}
