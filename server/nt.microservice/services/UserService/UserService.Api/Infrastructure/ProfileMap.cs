using UserService.Api.ViewModels.User;
using UserService.Domain.Entities;

namespace UserService.Api.Infrastructure;
public class ProfileMap:Profile
{
    public ProfileMap()
    {
        CreateMap<CreateUserRequestViewModel, UserMetaInformation>();
        CreateMap<UserMetaInformation, CreateUserResponseViewModel>();
    }
}
