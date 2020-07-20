﻿using Nt.Infrastructure.WebApi.ViewModels.Common;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.ResponseObjects
{
    public class CreateUserProfileResponse : IErrorInfo
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string ErrorMessage { get; set; }
    }
}