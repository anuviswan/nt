using Nt.WpfClient.Interfaces;

namespace Nt.WpfClient.ViewModels.Base;

public class NtMenuItemViewModelBase : NtPageViewModelBase, IHasMenuReference
{
    public virtual string Title { get; }
    public virtual NtPageViewModelBase ViewModel { get; }
    public virtual object Icon { get; }
}
