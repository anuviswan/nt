using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nt.WebApi.Exceptions;
using Nt.WebApi.Models.RequestObjects;
using Nt.WebApi.Models.ResponseObjects;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nt.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userService;

        public UserController(IMapper mapper, IUserRepository userRepository):base(mapper)
        {
            _userService = userRepository;
        }

        /// <summary>
        /// Retrieve List of Users
        /// </summary>
        /// <returns>List Of Users</returns>
        [HttpGet]
        public async Task<IEnumerable<UserProfileResponse>> Get()
        {
            var result = await _userService.GetAsync();
            return Mapper.Map<IEnumerable<UserProfileResponse>>(result);
        }

        /// <summary>
        /// Validate User Login Request
        /// </summary>
        /// <param name="loginRequest">Includes UserName and Base 64 encoded password</param>
        /// <returns>User Details if successfull login with IsAuthenticated Flag true. Invalid User with IsAuthenticated Flag false if validation fails</returns>
        [HttpPost]
        [Route("ValidateUser")]
        public async Task<LoginResponse> ValidateUser(LoginRequest loginRequest)
        {
            try
            {
                UserEntity userEntity = Mapper.Map<UserEntity>(loginRequest);
                var base64Key = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(userEntity.PassKey));
                var validUser = await _userService.ValidateUserAsync(userEntity.UserName, base64Key);
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

        /// <summary>
        /// Creates a new User with the specified details
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns User details if User is created sucessfully. Returns token with Error Message if User already exists with same username</returns>

        [HttpPost]
        [Route("Register")]

        public async Task<CreateUserProfileResponse> CreateUser(CreateUserProfileRequest user)
        {
            var userEntity = Mapper.Map<UserEntity>(user);
            if (await _userService.CheckIfUserExistsAsync(user.UserName.ToLower()))
            {
                var userReponse = Mapper.Map<CreateUserProfileResponse>(userEntity);
                userReponse.ErrorMessage = "User already exists";
                return userReponse;
            }
            else
            {
                userEntity.UserName = userEntity.UserName.ToLower();
                userEntity.PassKey = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(user.PassKey));
                var result = await _userService.CreateAsync(userEntity);
                return Mapper.Map<CreateUserProfileResponse>(result);
            }
        }
    }
}