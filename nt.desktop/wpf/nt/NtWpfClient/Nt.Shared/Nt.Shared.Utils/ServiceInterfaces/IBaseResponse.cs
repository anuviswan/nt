using System.Collections.Generic;

namespace Nt.Shared.Utils.ServiceInterfaces
{
    public interface IBaseResponse
    {
        IEnumerable<string> Errors { get; set; }
        bool HasError { get; }
    }
}