using AutoMapper;
using Nt.WebApi.Models;
using Nt.WebApi.Models.RequestObjects;
using Nt.WebApi.Models.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Profiles
{
    public class UserEntityProfile:Profile
    {
        public UserEntityProfile()
        {
            // TO UserEntity
            CreateMap<LoginRequest, UserEntity>();
            // From UserEntity
            CreateMap<UserEntity, UserProfileResponse>();
            CreateMap<UserEntity, LoginResponse>();
            
        }
    }
}
