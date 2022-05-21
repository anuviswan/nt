using System.ComponentModel.DataAnnotations;

namespace UserService.Api.ViewModels.UserManagement;

public record SearchUserRequestViewModel
{
    [Required(ErrorMessage = $"{nameof(SearchPart)} is mandatory.")]
    [MinLength(3, ErrorMessage = $"{nameof(SearchPart)} should be of minimum 3 characters")]
    public string SearchPart { get; set; }
}
