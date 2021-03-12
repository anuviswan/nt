using System;
using System.Collections.Generic;
using System.Text;
using Nt.Shared.Utils.ControlBase;

namespace Nt.Shared.Utils.ServiceInterfaces
{
    public interface IWindowService
    {
        bool? ShowDialog(ViewModelBase viewModel);
    }
}
