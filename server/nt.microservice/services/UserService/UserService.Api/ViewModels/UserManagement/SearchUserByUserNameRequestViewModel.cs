using System.ComponentModel.DataAnnotations;

namespace UserService.Api.ViewModels.UserManagement;

public record SearchUserByUserNameRequestViewModel
{
    [Required]
    public string UserName { get; set; }
}
