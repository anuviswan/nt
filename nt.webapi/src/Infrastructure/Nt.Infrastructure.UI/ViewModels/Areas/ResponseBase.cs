using Nt.Infrastructure.WebApi.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas
{
    public class ResponseBase : IErrorInfo
    {
        public bool HasError => Errors.Any();

        public List<string> Errors { get; set; } = new List<string>();
    }
}
