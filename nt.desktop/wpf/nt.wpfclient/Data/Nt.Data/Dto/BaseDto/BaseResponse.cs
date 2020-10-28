using Nt.Data.Contracts.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Data.Dto.BaseDto
{
    public class BaseResponse:IBaseResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public bool HasError => Errors.Any();
    }
}
