using Nt.Shared.Utils.Interfaces;

namespace Nt.Shared.Utils.ControlBase
{
    public class MenuItemViewModelBase : ViewModelBase, IHasMenuReference
    {
        public virtual string Name { get; set; }
        public virtual object Icon { get; set; }
        public virtual ViewModelBase ViewModel { get; }
    }
}
