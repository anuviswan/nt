﻿using Nt.Infrastructure.WebApi.ViewModels.Common;
using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser
{
    public class CreateUserProfileResponse : IErrorInfo
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    }
}
