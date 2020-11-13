using Nt.Utils.ControlInterfaces;
using Nt.WpfClient.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.WpfClient.Interfaces
{
    public interface IMenuItem
    {
        string Title { get; }
        NtPageViewModelBase ViewModel { get; }
        object Icon { get; }
    }
}
