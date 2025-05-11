using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Service.Command;
using UserService.Service.Dtos;
using UserService.Service.Query;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserService.Service.Infrastructure;

public class ProfileMap : Profile
{
    public ProfileMap()
    {
        CreateMap<string, SearchUserByUserNameQuery>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src));
        CreateMap<CreateUserCommand, UserMetaInformation>();
        CreateMap<UserProfileDto, UserMetaInformation>();
        CreateMap<UserMetaInformation, UserProfileDto>();
    }
}
