using UserService.Service.Dtos;

namespace UserService.Service.Query;

public class SearchUserQuery:IRequest<IEnumerable<UserMiniProfileDto>>
{
    public string? QueryPart { get; set; }

    public string? CurrentUserName { get; set; }
}
