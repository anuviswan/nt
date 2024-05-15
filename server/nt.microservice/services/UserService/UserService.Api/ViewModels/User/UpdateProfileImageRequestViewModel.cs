namespace UserService.Api.ViewModels.User;

public record UpdateProfileImageRequestViewModel
{
    public string UserName { get; set; }
    public FileData ImageData { get; set; }
}

public record FileData
{
    public string ImageKey { get; set; }
    public IFormFile File { get; set; }
}
public class UpdateProfileImageResponseViewModel
{

}
