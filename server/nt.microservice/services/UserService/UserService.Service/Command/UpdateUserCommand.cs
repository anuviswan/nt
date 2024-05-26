using UserService.Service.Dtos;

namespace UserService.Service.Command;

public class UpdateUserCommand:IRequest<UserProfileDto>
{
    public required string UserName { get; set; }
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
}
