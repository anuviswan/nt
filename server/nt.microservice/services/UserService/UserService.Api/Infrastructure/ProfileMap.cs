using UserService.Api.ViewModels.User;
using UserService.Domain.Entities;

namespace UserService.Api.Infrastructure;
public class ProfileMap:Profile
{
    public ProfileMap()
    {
        CreateMap<CreateUserRequestViewModel, User>();
        CreateMap<CreateUserRequestViewModel, UserMetaInformation>().ForMember(dto=>dto.User,opt=>opt.MapFrom(x=>x));
        CreateMap<ChangePasswordRequestViewModel,User>();

        CreateMap<UserMetaInformation, CreateUserResponseViewModel>().ForMember(dto=>dto.UserName,opt=>opt.MapFrom(x=>x.User.UserName));

    }
}
