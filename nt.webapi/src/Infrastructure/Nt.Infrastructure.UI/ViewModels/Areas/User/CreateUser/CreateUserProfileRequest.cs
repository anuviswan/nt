using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser
{
    public class CreateUserProfileRequest
    {
        [Required]
        public string UserName { get; set; }
        [MinLength(8, ErrorMessage = "Password should contain minimum of 8 characters"), Required(ErrorMessage = "Password cannot be empty")]
        public string PassKey { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
