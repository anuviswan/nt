using AutoMapper;
using Nt.WebApi.Models.ResponseObjects;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.ViewModels.RequestObjects;

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
