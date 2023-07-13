using Caliburn.Micro;
using Nt.Utils.ControlInterfaces;

namespace Nt.Utils.Controls.NtWindowManager;

public class NtWindowManagerViewModel:Conductor<NtViewModelBase>
{
    public NtViewModelBase _viewModel;
    public NtWindowManagerViewModel(NtViewModelBase viewModel)
    {
        _viewModel = viewModel;
        LoadControl();
    }

    private void LoadControl()
    {
        ActivateItem(_viewModel);
    }
}
