using System;
using System.Collections.Generic;
using System.Text;
using Nt.Controls.HelperControls.WindowManager;
using Nt.Shared.Utils.ControlBase;
using Nt.Shared.Utils.ServiceInterfaces;

namespace Nt.Shared.Utils.Services
{
    public class WindowService : IWindowService
    {
        public bool? ShowDialog(ViewModelBase viewModel)
        {
            var windowDialog = new WindowManagerDialog();
            windowDialog.DataContext = viewModel;
            return windowDialog.ShowDialog();
        }
    }
}
