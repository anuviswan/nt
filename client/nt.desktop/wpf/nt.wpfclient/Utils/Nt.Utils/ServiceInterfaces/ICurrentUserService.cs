using Nt.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Utils.ServiceInterfaces;

public interface ICurrentUserService
{
    Task<bool> Authenticate(string userName, string passKey, NtRef<string> errorMessage);
    bool IsAuthenticated { get; }

    string UserName { get; set; }
    string DisplayName { get; set; }
    string Bio { get; set; }
    string AuthToken { get; set; }
}
