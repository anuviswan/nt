using AuthService.Data.Repository;
using MediatR;

namespace AuthService.Service.Query;
public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery, bool>
{
    private readonly IUserRepository _userRepository;
    public ValidateUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.ValidateUser(request.User);
    }
}
