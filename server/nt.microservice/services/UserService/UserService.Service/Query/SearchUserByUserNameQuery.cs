using UserService.Service.Dtos;

namespace UserService.Service.Query;

public class SearchUserByUserNameQuery : IRequest<UserProfileDto?>
{
    public required string UserName { get; set; }
}
