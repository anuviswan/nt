using System.ComponentModel.DataAnnotations;

namespace UserService.Api.ViewModels.User;
public record CreateUserRequestViewModel
{
    [Required]
    [MinLength(6)]
    public string UserName { get; init; }
    [Required]
    [MinLength(8)]
    public string Password { get; init; }
    public string DisplayName { get; init; }
    public string Bio { get; init; }
}
