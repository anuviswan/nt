using AutoMapper;
using Nt.Domain.Entities.User;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ChangePassword;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.UpdateUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser;

namespace Nt.Infrastructure.WebApi.Profiles
{
    public class UserEntityProfile : BaseProfileMapper
    {
        public UserEntityProfile():base()
        {
         
        }

        protected override void ToEntity()
        {
            // TO UserEntity
            CreateMap<CreateUserProfileRequest, UserProfileEntity>();
            CreateMap<LoginRequest, UserProfileEntity>();
            CreateMap<UpdateUserProfileRequest, UserProfileEntity>();
            CreateMap<ChangePasswordRequest, UserProfileEntity>().ForMember(dest => dest.PassKey, opt => opt.MapFrom(src => src.OldPassword));
        }

        protected override void FromEntity()
        { 
            // From UserEntity
            CreateMap<UserProfileEntity, UserProfileResponse>();
            CreateMap<UserProfileEntity, CreateUserProfileResponse>().ReverseMap();
            CreateMap<UserProfileEntity, LoginResponse>();
        }
    }
}
