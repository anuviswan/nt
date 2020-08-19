using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Utils.ControlInterfaces
{
    public abstract class NtControlBase
    {

    }
    public abstract class NtControlBase<TViewModel> : NtControlBase where TViewModel: NtViewModelBase
    {
        private readonly Lazy<TViewModel> _viewModel;
        public NtControlBase()
        {
            _viewModel = new Lazy<TViewModel>(() => 
            {
                var vm = IoC.Get<TViewModel>();
                vm.Control = this;
                return vm;
            });
        }
        public TViewModel ViewModel => _viewModel.Value;
    }
}
