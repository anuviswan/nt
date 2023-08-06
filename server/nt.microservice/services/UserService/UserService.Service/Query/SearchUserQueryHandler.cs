using UserService.Service.Dtos;

namespace UserService.Service.Query;

public class SearchUserQueryHandler : IRequestHandler<SearchUserQuery, IEnumerable<UserProfileDto>>
{
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    public SearchUserQueryHandler(IUserMetaInformationRepository userMetaInformationRepository)
    {
        _userMetaInformationRepository = userMetaInformationRepository;
    }
    public async Task<IEnumerable<UserProfileDto>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request?.QueryPart);
        var userMetaInfo = await _userMetaInformationRepository.SearchUser(request.QueryPart).ConfigureAwait(false);
        return userMetaInfo.Select(x=> new UserProfileDto
        {
            DisplayName = x.DisplayName,
        });
    }
}
