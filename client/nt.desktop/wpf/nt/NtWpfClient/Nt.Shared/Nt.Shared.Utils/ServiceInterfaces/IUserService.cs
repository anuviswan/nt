using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nt.Shared.Utils.Helpers;

namespace Nt.Shared.Utils.ServiceInterfaces
{
    public interface IUserService
    {
        string UserName { get; set; }
        string DisplayName { get; set; }
        string AuthToken { get; set; }
        bool IsAuthenticated { get; }
        Task<bool> Authenticate(string userName, string password, AsyncRef<string> errorMessage);
    }
}
