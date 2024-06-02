using AutoMapper;
using UserService.Service.Dtos;

namespace UserService.Service.Query;

public class SearchUserByDisplayNameQueryHandler : IRequestHandler<SearchUserByDisplayNameQuery, IEnumerable<UserProfileDto>>
{
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    private readonly IMapper _mapper;
    public SearchUserByDisplayNameQueryHandler(IUserMetaInformationRepository userMetaInformationRepository,IMapper mapper)
    {
        _userMetaInformationRepository = userMetaInformationRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<UserProfileDto>> Handle(SearchUserByDisplayNameQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(request?.QueryPart);
        var users = await _userMetaInformationRepository.SearchUserByPartialDisplayName(request.QueryPart).ConfigureAwait(false);

        if(!users.Any()) return Enumerable.Empty<UserProfileDto>();

        return _mapper.Map<IEnumerable<UserProfileDto>>(users);
    }
}
