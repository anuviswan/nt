using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.ChangePassword
{
    public record ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; init; }
        [MinLength(8, ErrorMessage = "Password should contain minimum of 8 characters"), Required]
        public string NewPassword { get; init; }
    }
}
