using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Data.Contracts.Dto;

namespace Nt.Data.Types.Dto.BaseDto
{
    public class BaseResponse : IBaseResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public bool HasError => Errors?.Any() == true;
    }
}
