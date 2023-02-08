using AuthService.Data.Database;
using AuthService.Domain.Entities;
using AuthService.Service.Helpers.Exceptions;
using MediatR;

namespace AuthService.Service.Command;
public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public AddUserCommandHandler(IUnitOfWorkFactory uowFactory)
    {
        _unitOfWorkFactory = uowFactory;
    }
    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var uow = _unitOfWorkFactory.CreateUnitOfWork();
            var userNameExists = (await uow.UserRepository.GetAll())
                .Any(x => string.Equals(x.UserName, request.User.UserName, StringComparison.InvariantCultureIgnoreCase));

            if (userNameExists) throw new UserAlreadyExistsException();
            return await uow.UserRepository.AddAsync(request.User);
        }
        catch (Exception e)
        {

            throw;
        }
        
    }
}
