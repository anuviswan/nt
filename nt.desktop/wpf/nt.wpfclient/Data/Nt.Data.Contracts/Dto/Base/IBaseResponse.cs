using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Data.Contracts.Dto.Base
{
    public interface IBaseResponse
    {
        IEnumerable<string> Errors { get; set; }
        bool HasError { get; }
    }
}
