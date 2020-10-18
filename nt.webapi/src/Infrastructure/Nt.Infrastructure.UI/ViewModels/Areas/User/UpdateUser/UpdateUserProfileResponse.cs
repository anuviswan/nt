using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.UpdateUser
{
    public class UpdateUserProfileResponse:ResponseBase
    {
        public object ModelState { get; set; }
    }
}
