﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nt.WebApi.Exceptions;
using Nt.WebApi.Models.RequestObjects;
using Nt.WebApi.Models.ResponseObjects;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Shared.Settings;
using System;
using System.Collections.Generic;

namespace Nt.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userService;

        public UserController(IMapper mapper, IUserDatabaseSettings userDatabaseSettings,IUserRepository userRepository):base(mapper,userDatabaseSettings)
        {
            _userService = userRepository;
        }

        [HttpGet]
        public IEnumerable<UserProfileResponse> Get()
        {
            var result = _userService.Get();
            return Mapper.Map<IEnumerable<UserProfileResponse>>(result);
        }

        [HttpPost]
        [Route("ValidateUser")]
        public LoginResponse ValidateUser(LoginRequest loginRequest)
        {
            try
            {
                var userEntity = Mapper.Map<UserEntity>(loginRequest);
                var base64Key = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(userEntity.PassKey));
                var validUser = _userService.ValidateUser(userEntity.UserName, base64Key);
                var result = Mapper.Map<LoginResponse>(validUser);
                result.IsAuthenticated = true;
                result.LoginTime = DateTime.UtcNow;
                return result;
            }
            catch (Exception ex) when (ex is InvalidUserException)
            {

                return new LoginResponse { IsAuthenticated = false,ErrorMessage = "Invalid Username or Password" };
            }
        }

        [HttpPost]
        [Route("Register")]

        public CreateUserProfileResponse CreateUser(CreateUserProfileRequest user)
        {
            var userEntity = Mapper.Map<UserEntity>(user);
            if (_userService.CheckIfUserExists(user.UserName))
            {
                var userReponse = Mapper.Map<CreateUserProfileResponse>(userEntity);
                userReponse.ErrorMessage = "User already exists";
                return userReponse;
            }
            else
            {
                userEntity.PassKey = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(user.PassKey));
                var result = _userService.Create(userEntity);
                return Mapper.Map<CreateUserProfileResponse>(result);
            }
        }
    }
}