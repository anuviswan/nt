using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nt.WebApi.Models;
using Nt.WebApi.Services;

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
            if(userDto.UserName == "jia" && userDto.PassKey == "anu")
            {
                return new LoginDto
                {
                    UserName = userDto.UserName,
                    LoggedInTime = DateTime.Now.ToUniversalTime(),
                    Validated = true
                };
            }
            return new LoginDto { Validated = false };
        }
    }
}