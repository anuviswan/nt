using AuthService.Data.Repository;
using AuthService.Domain.Entities;
using MediatR;

namespace AuthService.Service.Query;
public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery, User>
{
    private readonly IUserRepository _userRepository;
    public ValidateUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.ValidateUser(request.User);
    }
}
