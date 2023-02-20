namespace UserService.Service.Command;

public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, UserMetaInformation>
{
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    public RemoveUserCommandHandler(IUserMetaInformationRepository userMetaInformationRepository)
    {
        _userMetaInformationRepository = userMetaInformationRepository;
    }

    public async Task<UserMetaInformation> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var userToBeDeleted = await _userMetaInformationRepository.GetUser(request.UserName);

        if (userToBeDeleted != null)
        {
            return await _userMetaInformationRepository.DeleteAsync(userToBeDeleted);
        }

        throw new ArgumentOutOfRangeException(request.UserName);
    }
}
