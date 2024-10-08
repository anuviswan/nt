﻿using AuthService.Data.Database;
using AuthService.Data.Interfaces.Entities;
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
        ArgumentNullException.ThrowIfNullOrEmpty(request.User?.UserName);
        ArgumentNullException.ThrowIfNullOrEmpty(request.User?.Password);
        
        using var uow = _unitOfWorkFactory.CreateUnitOfWork();
        var currentUsers = await uow.UserRepository.GetAll().ConfigureAwait(false);
        var userNameExists = currentUsers.Any(x => string.Equals(x.UserName, request.User!.UserName, StringComparison.InvariantCultureIgnoreCase));

        if (userNameExists) throw new UserAlreadyExistsException();
        return await uow.UserRepository.AddAsync(request.User!).ConfigureAwait(false);

    }
}
