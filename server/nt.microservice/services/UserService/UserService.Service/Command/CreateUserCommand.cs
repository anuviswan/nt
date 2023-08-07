namespace UserService.Service.Command;

public class CreateUserCommand:IRequest<UserMetaInformation>
{
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Bio { get; set; }
}
