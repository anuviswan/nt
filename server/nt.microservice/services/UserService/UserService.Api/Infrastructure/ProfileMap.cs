using UserService.Api.ViewModels.User;
using UserService.Api.ViewModels.UserManagement;
using UserService.Domain.Entities;
using UserService.Service.Dtos;
using UserService.Service.Query;

namespace UserService.Api.Infrastructure;
public class ProfileMap:Profile
{
    public ProfileMap()
    {
        CreateMap<CreateUserRequestViewModel, UserMetaInformation>().ReverseMap();
        CreateMap<UserMetaInformation, CreateUserResponseViewModel>().ReverseMap();

        CreateMap<UserProfileDto, UserProfileViewModel>().ReverseMap();

        CreateMap<SearchUserByUserNameRequestViewModel, SearchUserByUserNameQuery>().ReverseMap();
        CreateMap<UserProfileViewModel, UserProfileDto>().ReverseMap();

    }
}
