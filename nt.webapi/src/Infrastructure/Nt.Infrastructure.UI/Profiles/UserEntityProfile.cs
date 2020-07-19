using AutoMapper;
using Nt.Domain.Entities.User;
using Nt.Infrastructure.WebApi.ViewModels.RequestObjects;
using Nt.Infrastructure.WebApi.ViewModels.ResponseObjects;

namespace Nt.Infrastructure.WebApi.Profiles
{
    public class UserEntityProfile : Profile
    {
        public UserEntityProfile()
        {
            // TO UserEntity
            CreateMap<LoginRequest, UserProfileEntity>();
            CreateMap<CreateUserProfileRequest, UserProfileEntity>();
            // From UserEntity
            CreateMap<UserProfileEntity, UserProfileResponse>();
            CreateMap<UserProfileEntity, LoginResponse>();
            CreateMap<UserProfileEntity, CreateUserProfileResponse>();

        }
    }
}
