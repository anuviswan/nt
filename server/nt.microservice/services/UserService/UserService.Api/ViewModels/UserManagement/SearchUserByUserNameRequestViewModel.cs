using System.ComponentModel.DataAnnotations;

namespace UserService.Api.ViewModels.UserManagement;

public record SearchUserByUserNameRequestViewModel
{
    [Required(ErrorMessage = $"{nameof(UserName)} is mandatory.")]
    public string UserName { get; set; }
}
