namespace UserService.Api.ViewModels.User;

public record UpdateProfileImageRequestViewModel
{
    public string ImageKey { get; set; }
    public IFormFile File { get; set; }
}

public class UpdateProfileImageResponseViewModel
{

}
