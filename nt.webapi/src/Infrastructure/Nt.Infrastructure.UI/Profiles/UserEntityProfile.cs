using AutoMapper;
using Nt.Domain.Entities.User;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.UpdateUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser;

namespace Nt.Infrastructure.WebApi.Profiles
{
    public class UserEntityProfile : Profile
    {
        public UserEntityProfile()
        {

            ToUserProfileEntity();
            FromUserProfileEntity();
        }

        public void ToUserProfileEntity()
        {
            // TO UserEntity
            CreateMap<CreateUserProfileRequest, UserProfileEntity>();
            CreateMap<LoginRequest, UserProfileEntity>();
            CreateMap<UpdateUserProfileRequest, UserProfileEntity>();
        }

        public void FromUserProfileEntity()
        { 
            // From UserEntity
            CreateMap<UserProfileEntity, UserProfileResponse>();
            CreateMap<UserProfileEntity, CreateUserProfileResponse>().ReverseMap();
            CreateMap<UserProfileEntity, LoginResponse>();

        }
    }
}
