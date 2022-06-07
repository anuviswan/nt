using System.ComponentModel.DataAnnotations;

namespace UserService.Api.ViewModels.User;
public record CreateUserRequestViewModel
{
    [Required(ErrorMessage = $"{nameof(UserName)} is mandatory.")]
    [MinLength(6,ErrorMessage = $"{nameof(UserName)} should be minimum 6 characters.")]
    [MaxLength(15, ErrorMessage = $"{nameof(UserName)} should be maximum 15 characters.")]
    public string UserName { get; init; }
    public string DisplayName { get; init; }
    public string Bio { get; init; }
}
