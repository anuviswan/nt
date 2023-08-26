using AutoMapper;
using UserService.Service.Dtos;

namespace UserService.Service.Query;

public class SearchUserByUserNameQueryHandler : IRequestHandler<SearchUserByUserNameQuery, UserProfileDto?>
{
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    private readonly IMapper _mapper;
    public SearchUserByUserNameQueryHandler(IUserMetaInformationRepository userMetaInformationRepository, IMapper mapper)
    {
        _userMetaInformationRepository = userMetaInformationRepository;
        _mapper = mapper;
    }
    public async Task<UserProfileDto?> Handle(SearchUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(request.UserName);
        
        var userMetaInfo = await _userMetaInformationRepository.GetUser(request.UserName).ConfigureAwait(false);

        return userMetaInfo == null ? null : _mapper.Map<UserProfileDto>(userMetaInfo);
    }
}
