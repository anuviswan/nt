using AuthService.Data.Database;
using AuthService.Data.Repository;
using AuthService.Domain.Entities;
using MediatR;

namespace AuthService.Service.Query;
public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery, User>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public ValidateUserQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }
    public async Task<User> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
    {
        using var uow = _unitOfWorkFactory.CreateUnitOfWork();
        return await uow.UserRepository.ValidateUserAsync(request.User);
    }
}
