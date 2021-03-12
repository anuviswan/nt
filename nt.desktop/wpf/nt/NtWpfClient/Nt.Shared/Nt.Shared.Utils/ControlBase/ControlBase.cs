using System;
using System.Collections.Generic;
using System.Text;
using Nt.Shared.Utils.Helpers;

namespace Nt.Shared.Utils.ControlBase
{
    public abstract class ControlBase:PropertyChangedBase
    {

    }


    public abstract class ControlBase<TViewModel>:ControlBase where TViewModel : ViewModelBase
    {

    }
}
