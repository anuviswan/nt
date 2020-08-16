using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Data.Dto.BaseDto
{
    public class BaseResponse
    {
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
    }
}
