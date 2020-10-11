using Nt.Infrastructure.WebApi.ViewModels.Common;
using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser
{
    public class CreateUserProfileResponse : ResponseBase
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}
