namespace UserService.Service.Command;

public class CreateUserCommand:IRequest<UserMetaInformation>
{
    public UserMetaInformation UserProfile { get; set; }
    public User UserCredential { get; set; }
}
