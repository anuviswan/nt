using UserService.Service.Dtos;

namespace UserService.Service.Command;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserProfileDto>
{
    public Task<UserProfileDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}