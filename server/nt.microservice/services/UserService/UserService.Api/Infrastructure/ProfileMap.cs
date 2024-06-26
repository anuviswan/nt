﻿using nt.shared.dto.User;
using UserService.Api.ViewModels.User;
using UserService.Api.ViewModels.UserManagement;
using UserService.Service.Dtos;
using UserService.Service.Query;

namespace UserService.Api.Infrastructure;
public class ProfileMap:Profile
{
    public ProfileMap()
    {

        CreateMap<CreateUserRequestViewModel, CreateUserCommand>().ReverseMap();
        CreateMap<UserProfileDto, CreateUserResponseViewModel>().ReverseMap();
        CreateMap<CreateUserRequestViewModel, CreateUserInitiated>();


        CreateMap<UserProfileDto, UserProfileViewModel>().ReverseMap();

        CreateMap<SearchUserByUserNameRequestViewModel, SearchUserByUserNameQuery>().ReverseMap();
        CreateMap<UserProfileViewModel, UserProfileDto>().ReverseMap();

        CreateMap<IEnumerable<UserProfileDto>, SearchUserByDisplayNameResponseViewModel>().AfterMap((src, dest, cntxt) =>
        {
            dest.Users = cntxt.Mapper.Map<IEnumerable<UserProfileViewModel>>(src);
        });

        CreateMap<UserProfileDto, SearchUserByUserNameResponseViewModel>().AfterMap((src, dest, cntxt) =>
        {
            dest.User = cntxt.Mapper.Map<UserProfileViewModel>(src);
        });

        CreateMap<UpdateProfileImageRequestViewModel, UploadProfileImageCommand>();

        CreateMap<UpdateUserRequestViewModel, UpdateUserCommand>();
        CreateMap<UserProfileDto, UpdateUserResponseViewModel>().ReverseMap();
    }
}
