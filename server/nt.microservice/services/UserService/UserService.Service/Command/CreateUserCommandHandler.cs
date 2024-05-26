using AutoMapper;
using UserService.Service.Dtos;

namespace UserService.Service.Command;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserProfileDto>
{
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    private readonly IMapper _mapper;
    public CreateUserCommandHandler(IUserMetaInformationRepository userMetaInformationRepository, IMapper mapper)
    {
        _userMetaInformationRepository = userMetaInformationRepository;
        _mapper = mapper;
    }
    public async Task<UserProfileDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(request?.UserName);

        var userFound = await _userMetaInformationRepository.GetUser(request.UserName).ConfigureAwait(false);

        if (userFound is not null)
        {
            throw new ArgumentException("User already exist");
        }

        var response = await _userMetaInformationRepository.AddAsync(_mapper.Map<UserMetaInformation>(request)).ConfigureAwait(false);
        
        return _mapper.Map<UserProfileDto>(response);
    }
}
