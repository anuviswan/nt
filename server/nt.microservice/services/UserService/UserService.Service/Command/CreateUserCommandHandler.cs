namespace UserService.Service.Command;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserMetaInformation>
{
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    public CreateUserCommandHandler(IUserMetaInformationRepository userMetaInformationRepository)
    {
        _userMetaInformationRepository = userMetaInformationRepository;
    }
    public async Task<UserMetaInformation> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request?.UserProfile);
        var response = await _userMetaInformationRepository.AddAsync(request.UserProfile);
        if(response.Id > 0)
        {

        }
        return response;
    }
}
