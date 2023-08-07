using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Service.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserService.Service.Infrastructure;

public class ProfileMap : Profile
{
    public ProfileMap()
    {
        CreateMap<UserProfileDto, UserMetaInformation>();
        CreateMap<UserMetaInformation, UserProfileDto>();
    }
}
