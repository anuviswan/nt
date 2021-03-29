using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Data.Contracts.Dto
{
    public interface IBaseResponse
    {
        IEnumerable<string> Errors { get; set; }
        bool HasError { get; }
    }
}
