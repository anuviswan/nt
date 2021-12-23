namespace UserService.Service.Command;
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand,bool>
{
    private readonly IUserMetaInformationRepository _userMetaInformationRepository;
    public ChangePasswordCommandHandler(IUserMetaInformationRepository userMetaInformationRepository)
    {
        _userMetaInformationRepository = userMetaInformationRepository;
    }
    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userMetaInformationRepository.GetUser(request.UserToUpdate.UserName);
        user.User.Password = request.UserToUpdate.Password;

        var updatedUser = await _userMetaInformationRepository.UpdateAsync(user);

        return (updatedUser.User.Password == request.UserToUpdate.Password);
    }
}
