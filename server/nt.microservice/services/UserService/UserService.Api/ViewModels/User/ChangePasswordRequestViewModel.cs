using System.ComponentModel.DataAnnotations;

namespace UserService.Api.ViewModels.User;
public record ChangePasswordRequestViewModel
{
    [Required(ErrorMessage = $"{nameof(UserName)} is mandatory.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = $"{nameof(Password)} is mandatory.")]
    [MinLength(6, ErrorMessage = $"{nameof(Password)} should be minimum 6 characters.")]
    [MaxLength(15, ErrorMessage = $"{nameof(Password)} should be maximum 15 characters.")]
    public string Password { get; set; }
}
