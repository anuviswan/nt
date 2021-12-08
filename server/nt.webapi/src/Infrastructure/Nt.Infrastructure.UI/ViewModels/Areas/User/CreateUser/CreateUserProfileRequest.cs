using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser;
public record CreateUserProfileRequest
{
    [Required,MinLength(8, ErrorMessage = "username should contain minimum of 8 characters")]
    public string UserName { get; init; }
    [MinLength(8, ErrorMessage = "Password should contain minimum of 8 characters"), Required(ErrorMessage = "Password cannot be empty")]
    public string PassKey { get; init; }
    public string DisplayName { get; init; }
    public string Bio { get; init; }
}
