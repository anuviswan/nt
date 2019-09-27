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

        public ActionResult<LoginDto> ValidateUser(string userName,string password)
        {
            if(userName == "jia" && password == "anu")
            {
                return new LoginDto
                {
                    UserName = userName,
                    LoggedInTime = DateTime.Now.ToUniversalTime(),
                    Validated = true
                };
            }
            return new LoginDto { Validated = false };
        }
    }
}