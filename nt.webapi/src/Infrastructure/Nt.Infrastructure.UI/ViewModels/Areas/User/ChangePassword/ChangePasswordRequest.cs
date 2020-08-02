using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.ChangePassword
{
    public class ChangePasswordRequest
    {
        [MinLength(8, ErrorMessage ="Password should contain minimum of 8 characters"),Required,]
        public string OldPassword { get; set; }
        [MinLength(8, ErrorMessage = "Password should contain minimum of 8 characters"), Required]
        public string NewPassword { get; set; }
    }
}
