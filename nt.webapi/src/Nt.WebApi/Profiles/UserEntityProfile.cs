using AutoMapper;
using Nt.WebApi.Models.RequestObjects;
using Nt.WebApi.Models.ResponseObjects;
using Nt.WebApi.Shared.Entities;

namespace Nt.WebApi.Profiles
{
    public class UserEntityProfile:Profile
    {
        public UserEntityProfile()
        {
            // TO UserEntity
            CreateMap<LoginRequest, UserEntity>();
            CreateMap<CreateUserProfileRequest, UserEntity>();
            // From UserEntity
            CreateMap<UserEntity, UserProfileResponse>();
            CreateMap<UserEntity, LoginResponse>();
            CreateMap<UserEntity, CreateUserProfileResponse>();
            
        }
    }
}
