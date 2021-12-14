namespace UserService.Service.Command;
public class CreateUserCommand:IRequest<UserMetaInformation>
{
    public UserMetaInformation User { get; set; }
}
