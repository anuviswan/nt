using UserService.Service.Dtos;

namespace UserService.Service.Command;

public class CreateUserCommand:IRequest<UserProfileDto>
{
    public required string UserName { get; init; }
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
}
