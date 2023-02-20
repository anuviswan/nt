namespace UserService.Service.Command;

public class RemoveUserCommand : IRequest<UserMetaInformation>
{
    public string UserName { get; set; } = null!;
}
