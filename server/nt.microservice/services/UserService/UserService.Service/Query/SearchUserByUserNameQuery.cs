using UserService.Service.Dtos;

namespace UserService.Service.Query;

public class SearchUserByUserNameQuery : IRequest<UserProfileDto?>
{
    public string UserName { get; set; }
}
