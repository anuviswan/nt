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
        ArgumentNullException.ThrowIfNull(request?.UserName);

        var userFound = await _userMetaInformationRepository.GetUser(request.UserName).ConfigureAwait(false);

        if (userFound is not null)
        {
            throw new ArgumentException("User already exist");
        }

        var response = await _userMetaInformationRepository.AddAsync(new UserMetaInformation 
        { 
            UserName = request.UserName,
            DisplayName = request.DisplayName,
            Bio = request.Bio
        }).ConfigureAwait(false);
        
        return response;
    }
}
