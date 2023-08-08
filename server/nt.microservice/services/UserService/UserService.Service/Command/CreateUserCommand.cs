using UserService.Service.Dtos;

namespace UserService.Service.Command;

public class CreateUserCommand:IRequest<UserProfileDto>
{
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Bio { get; set; }
}
