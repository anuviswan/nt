using Nt.Utils.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Utils.ControlInterfaces
{
    public class NtViewModelWithHttpService<TControl>:NtViewModelBase<TControl> where TControl : NtControlBase
    {
        private readonly IHttpService _httpService;
        public NtViewModelWithHttpService(IHttpService httpService):base()
        {
            _httpService = httpService;
        }

        protected IHttpService HttpService => _httpService;
    }
}
