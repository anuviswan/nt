using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.UpdateUser
{
    public class UpdateUserProfileRequest
    {

        [Required, MaxLength(30, ErrorMessage = "Display Name should be less than 30 characters")]
        public string DisplayName { get; set; }
        [Required, MaxLength(180, ErrorMessage = "Bio should be less than 180 characters")]
        public string Bio { get; set; }
    }
}
