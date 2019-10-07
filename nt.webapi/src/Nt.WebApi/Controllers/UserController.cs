using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nt.WebApi.Exceptions;
using Nt.WebApi.Interfaces.Services;
using Nt.WebApi.Models;
using Nt.WebApi.Models.ResponseObjects;
using Nt.WebApi.Services;
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
            return _mapper.Map<IEnumerable<UserProfileResponse>>(result);
        }

        [HttpPost]
        [Route("ValidateUser")]
        public ActionResult<LoginDto> ValidateUser(UserEntity userDto)
        {
            try
            {
                var base64Key = Convert.FromBase64String(userDto.PassKey);
                var passKey = System.Text.Encoding.ASCII.GetString(base64Key);
                var validUser = _userService.ValidateUser(userDto.UserName, passKey);
                return new LoginDto
                {
                    LoggedInTime = DateTime.Now,
                    Id = validUser.Id,
                    DisplayName = validUser.DisplayName,
                    Validated = true
                };
            }
            catch (Exception ex) when (ex is InvalidUserException)
            {

                return new LoginDto { Validated = false,ErrorMessage = "Invalid Username or Password" };
            }
        }

        [HttpPost]
        [Route("Register")]

        public ActionResult<UserEntity> CreateUser(UserEntity user)
        {
            //if (_userService.CheckIfUserExists(user.UserName))
            //{
            //    //user.ErrorMessage = "User already exists";
            //    return user;
            //}
            //else
            //{
            //    user.PassKey = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(user.PassKey));
            //    return _userService.Create(user);
            //}
            return default;
        }
    }
}