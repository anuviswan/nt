using Microsoft.AspNetCore.Mvc;
using Nt.WebApi.Exceptions;
using Nt.WebApi.Models;
using Nt.WebApi.Services;
using System;
using System.Collections.Generic;

namespace Nt.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> Get() => _userService.Get();

        [HttpPost]
        [Route("ValidateUser")]
        public ActionResult<LoginDto> ValidateUser(UserDto userDto)
        {
            try
            {
                var base64Key = Convert.FromBase64String(userDto.PassKey);
                var passKey = System.Text.Encoding.ASCII.GetString(base64Key);
                var validUser = _userService.Validate(userDto.UserName, passKey);
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

        public ActionResult<UserDto> CreateUser(UserDto user)
        {
            if (_userService.CheckIfUserExists(user.UserName))
            {
                user.ErrorMessage = "User already exists";
                return user;
            }
            else
            {
                user.PassKey = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(user.PassKey));
                return _userService.Create(user);
            }
        }
    }
}