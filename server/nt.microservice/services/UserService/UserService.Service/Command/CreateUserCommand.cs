namespace UserService.Service.Command;

public class CreateUserCommand:IRequest<UserMetaInformation>
{
    public UserMetaInformation UserProfile { get; set; } = null!;
    public User UserCredential { get; set; } = null!;
}
