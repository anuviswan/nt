using System;
using System.Collections.Generic;
using System.Text;
using Nt.Shared.Utils.Helpers;

namespace Nt.Shared.Utils.ControlBase
{
    public abstract class ControlBase:PropertyChangedBase
    {

    }


    public abstract class ControlBase<TViewModel>:ControlBase where TViewModel : ViewModelBase, new()
    {
        private readonly Lazy<TViewModel> _viewModel;
        public ControlBase()
        {
            _viewModel = new Lazy<TViewModel>(() =>
            {
                var vm = new TViewModel(); // TODO: Replace with IOC later
                vm.Control = this;
                return vm;
            });
        }
        public TViewModel ViewModel => _viewModel.Value;
    }
}
