using UserService.Service.Dtos;

namespace UserService.Service.Query;

public class SearchUserByDisplayNameQueryHandler : IRequestHandler<SearchUserByDisplayNameQuery, IEnumerable<UserProfileDto>>
{
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    public SearchUserByDisplayNameQueryHandler(IUserMetaInformationRepository userMetaInformationRepository)
    {
        _userMetaInformationRepository = userMetaInformationRepository;
    }
    public async Task<IEnumerable<UserProfileDto>> Handle(SearchUserByDisplayNameQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request?.QueryPart);
        var userMetaInfo = await _userMetaInformationRepository.SearchUserByPartialDisplayName(request.QueryPart).ConfigureAwait(false);
        return userMetaInfo.Select(x=> new UserProfileDto
        {
            UserName = x.UserName,
            DisplayName = x.DisplayName,
        });
    }
}
