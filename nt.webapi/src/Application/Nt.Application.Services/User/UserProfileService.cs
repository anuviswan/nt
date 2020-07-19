﻿using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.User;
using System;
using System.Threading.Tasks;

namespace Nt.Application.Services.User
{
    public class UserProfileService : ServiceBase, IUserProfileService
    {
        public UserProfileService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<UserProfileEntity> AuthenticateAsync(string userName, string base64Key)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileEntity> ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileEntity> CreateUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileEntity> UpdateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
