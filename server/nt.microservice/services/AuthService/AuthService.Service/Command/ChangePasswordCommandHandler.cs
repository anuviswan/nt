using AuthService.Data.Database;
using AuthService.Data.Interfaces.Entities;
using AuthService.Service.Exceptions;
using MediatR;

namespace AuthService.Service.Command;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public ChangePasswordCommandHandler(IUnitOfWorkFactory uowFactory)
    {
        _unitOfWorkFactory = uowFactory;
    }
    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(request.UserName);
        ArgumentNullException.ThrowIfNullOrEmpty(request.OldPassword);
        ArgumentNullException.ThrowIfNullOrEmpty(request.NewPassword);

        using var uow = _unitOfWorkFactory.CreateUnitOfWork();
        var user = await uow.UserRepository.ValidateUserAsync(new User
        {
            UserName = request.UserName,
            Password = request.OldPassword,
        }).ConfigureAwait(false);

        if (user is null)
        {
            throw new IncorrectPasswordException();
        }

        return await uow.UserRepository.ChangePasswordAsync(user, request.NewPassword).ConfigureAwait(false);
    }
}
