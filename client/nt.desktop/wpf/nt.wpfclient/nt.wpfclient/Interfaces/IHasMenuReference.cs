using Nt.WpfClient.ViewModels.Base;

namespace Nt.WpfClient.Interfaces;

public interface IHasMenuReference
{
    string Title { get; }
    object Icon { get; }
}
