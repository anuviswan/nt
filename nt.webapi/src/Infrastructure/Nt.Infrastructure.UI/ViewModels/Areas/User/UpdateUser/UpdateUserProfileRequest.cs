using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.UpdateUser
{
    public record UpdateUserProfileRequest
    {

        [Required, MaxLength(30, ErrorMessage = "Display Name should be less than 30 characters")]
        public string DisplayName { get; init; }
        [Required, MaxLength(180, ErrorMessage = "Bio should be less than 180 characters")]
        public string Bio { get; init; }
    }
}
