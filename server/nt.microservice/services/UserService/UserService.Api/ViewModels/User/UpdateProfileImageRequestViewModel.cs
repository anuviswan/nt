using System.ComponentModel.DataAnnotations;

namespace UserService.Api.ViewModels.User;

public record UpdateProfileImageRequestViewModel
{
    [Required(ErrorMessage = $"{nameof(ImageKey)} is mandatory.")]
    public required string ImageKey { get; set; }
    [Required(ErrorMessage = $"{nameof(File)} is mandatory.")]
    public required IFormFile File { get; set; }
}

public class UpdateProfileImageResponseViewModel
{

}
