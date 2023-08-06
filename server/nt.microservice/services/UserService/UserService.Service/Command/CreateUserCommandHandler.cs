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
        ArgumentNullException.ThrowIfNull(request?.User);

        var userFound = await _userMetaInformationRepository.GetUser(request.User.UserName).ConfigureAwait(false);

        if (userFound is not null)
        {
            throw new ArgumentException("User already exist");
        }

        var response = await _userMetaInformationRepository.AddAsync(request.User).ConfigureAwait(false);
        
        return response;
    }
}
