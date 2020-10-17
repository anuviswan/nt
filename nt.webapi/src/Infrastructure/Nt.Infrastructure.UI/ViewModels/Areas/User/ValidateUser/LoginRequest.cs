﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassKey { get; set; }
    }
}
