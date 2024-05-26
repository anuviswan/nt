using AutoMapper;
using UserService.Service.Dtos;

namespace UserService.Service.Command;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserProfileDto>
{
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    private readonly IMapper _mapper;
    public UpdateUserCommandHandler(IUserMetaInformationRepository userMetaInformationRepository, IMapper mapper)
    {
        _userMetaInformationRepository = userMetaInformationRepository;
        _mapper = mapper;
    }
    public async Task<UserProfileDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request?.UserName);

        var userFound = await _userMetaInformationRepository.GetUser(request.UserName).ConfigureAwait(false);

        if (userFound is null)
        {
            throw new ArgumentException("Cannot find User");
        }

        userFound.DisplayName = request.DisplayName;
        userFound.Bio = request.Bio;


        var response = await _userMetaInformationRepository.UpdateAsync(userFound).ConfigureAwait(false);

        return _mapper.Map<UserProfileDto>(response);
    }
}