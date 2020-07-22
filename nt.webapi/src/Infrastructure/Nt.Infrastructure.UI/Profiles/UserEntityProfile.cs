using AutoMapper;
using Nt.Domain.Entities.User;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.RequestObjects;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ResponseObjects;

namespace Nt.Infrastructure.WebApi.Profiles
{
    public class UserEntityProfile : Profile
    {
        public UserEntityProfile()
        {
            // TO UserEntity
            CreateMap<CreateUserProfileRequest, UserProfileEntity>();
            // From UserEntity
            CreateMap<UserProfileEntity, UserProfileResponse>();
            CreateMap<UserProfileEntity, CreateUserProfileResponse>().ReverseMap();

        }
    }
}
