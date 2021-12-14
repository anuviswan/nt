namespace UserService.Service.Command;
public class CreateUserCommand:IRequest<User>
{
    public UserMetaInformation User { get; set; }
}
