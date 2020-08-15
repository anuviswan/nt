using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Utils.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        public bool IsAuthenticated { get; }

        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}
