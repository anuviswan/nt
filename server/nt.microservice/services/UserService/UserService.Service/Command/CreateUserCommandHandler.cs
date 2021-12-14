namespace UserService.Service.Command;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserMetaInformation>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    public CreateUserCommandHandler(IUserRepository userRepository,IUserMetaInformationRepository userMetaInformationRepository)
    {
        (_userRepository, _userMetaInformationRepository) = (userRepository,userMetaInformationRepository);
    }
    public async Task<UserMetaInformation> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userMetaInformationRepository.AddAsync(request.User);
    }
}
