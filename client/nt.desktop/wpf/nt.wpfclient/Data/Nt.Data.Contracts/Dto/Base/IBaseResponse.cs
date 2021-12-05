using System.Collections.Generic;

namespace Nt.Data.Contracts.Dto.Base
{
    public interface IBaseResponse
    {
        IEnumerable<string> Errors { get; set; }
        bool HasError { get; }
    }
}
