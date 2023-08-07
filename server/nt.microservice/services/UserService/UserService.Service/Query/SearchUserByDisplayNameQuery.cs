using UserService.Service.Dtos;

namespace UserService.Service.Query;

public class SearchUserByDisplayNameQuery:IRequest<IEnumerable<UserProfileDto>>
{
    public string? QueryPart { get; set; }

    public string? CurrentUserName { get; set; }
}
