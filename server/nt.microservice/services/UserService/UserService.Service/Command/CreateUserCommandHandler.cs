namespace UserService.Service.Command;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    public CreateUserCommandHandler(IUserRepository userRepository,IUserMetaInformationRepository userMetaInformationRepository)
    {
        (_userRepository, _userMetaInformationRepository) = (userRepository,userMetaInformationRepository);
    }
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userMetaInformationRepository.AddAsync(request.User);
        return result.User;
    }
}
