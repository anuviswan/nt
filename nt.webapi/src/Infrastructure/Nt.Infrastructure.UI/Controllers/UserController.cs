using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Nt.Domain.Entities.User;
using System.Threading.Tasks;
using Nt.Domain.ServiceContracts;
using Nt.Domain.ServiceContracts.User;
using System.Runtime.CompilerServices;
using Nt.Domain.Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Nt.Infrastructure.WebApi.Authentication;
using Microsoft.Extensions.Configuration;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.UpdateUser;
using System.Web.Http.ModelBinding;
using System.Linq;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ChangePassword;

namespace Nt.Infrastructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IUserManagementService _userManagementService;
        private readonly ITokenGenerator _tokenGenerator;

        public UserController(IMapper mapper, IUserProfileService userProfileService,IUserManagementService userManagementService,ITokenGenerator tokenGenerator) : base(mapper)
        {
            (_userProfileService, _userManagementService,_tokenGenerator) = (userProfileService, userManagementService,tokenGenerator);
        }

        /// <summary>
        /// Get all Users in the system
        /// </summary>
        /// <returns>User List</returns>
        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize]
        public async Task<IEnumerable<UserProfileResponse>> GetAll()
        {
            var usersFound = await _userManagementService.GetAllUsersAsync();
            return Mapper.Map<IEnumerable<UserProfileResponse>>(usersFound);
        }

        /// <summary>
        /// Validate User Login Request
        /// </summary>
        /// <param name="partialString">Partial username to be searched</param>
        /// <returns>Collection of Users who matches partial user name</returns>
        [HttpGet]
        [Route("SearchUser")]
        public async Task<IEnumerable<UserProfileResponse>> SearchUser(string partialString)
        {
            var usersFound = await _userManagementService.SearchUserAsync(partialString);
            return Mapper.Map<IEnumerable<UserProfileResponse>>(usersFound);
        }

        /// <summary>
        /// Validate User Login Request
        /// </summary>
        /// <param name="loginRequest">Includes UserName and Base 64 encoded password</param>
        /// <returns>User Details if successfull login with IsAuthenticated Flag true. Invalid User with IsAuthenticated Flag false if validation fails</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ValidateUser")]
        public async Task<LoginResponse> ValidateUser(LoginRequest loginRequest)
        {
            try
            {
                UserProfileEntity userEntity = Mapper.Map<UserProfileEntity>(loginRequest);
                var validUser = await _userProfileService.AuthenticateAsync(userEntity);
                var tokenString = _tokenGenerator.Generate(validUser);
                var result = Mapper.Map<LoginResponse>(validUser);
                result.Token = tokenString;
                result.IsAuthenticated = true;
                result.LoginTime = DateTime.UtcNow;
                return result;
            }
            catch (InvalidPasswordOrUsernameException ex)
            {
                return new LoginResponse { IsAuthenticated = false, ErrorMessage = ex.Message };
            }
            catch
            {
                return new LoginResponse { IsAuthenticated = false, ErrorMessage = "Unknown Exception" };
            }
        }

        /// <summary>
        /// Creates a new User with the specified details
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns User details if User is created sucessfully. Returns token with Error Message if User already exists with same username</returns>
        [HttpPost]
        [Route("CreateUser")]
        [Authorize]
        public async Task<CreateUserProfileResponse> CreateUser(CreateUserProfileRequest user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.PassKey))
                {
                    var userReponse = new CreateUserProfileResponse
                    {
                        UserName = user.UserName,
                        DisplayName = user.DisplayName
                    };
                    userReponse.ErrorMessage = "Password cannot be empty or whitespace";
                    return userReponse;
                }
                var userEntity = Mapper.Map<UserProfileEntity>(user);

                userEntity.UserName = userEntity.UserName.ToLower();
                userEntity.PassKey = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(user.PassKey));
                var result = await _userProfileService.CreateUserAsync(userEntity);
                return Mapper.Map<CreateUserProfileResponse>(result);
            }
            catch (UserNameExistsException e)
            {
                var userReponse = new CreateUserProfileResponse
                {
                    UserName = user.UserName,
                    DisplayName = user.DisplayName
                };
                userReponse.ErrorMessage = "User already exists";
                return userReponse;
            }
        }

        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <param name="user">User Profile to update</param>
        /// <returns>Updated User Profile</returns>
        [HttpPost]
        [Route("UpdateUser")]
        [Authorize]
        public async Task<UpdateUserProfileResponse> UpdateUser(UpdateUserProfileRequest user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userName = User.Identity.Name;
                    var userProfileEntity = Mapper.Map<UserProfileEntity>(user);
                    userProfileEntity.UserName = userName;

                    var result = await _userProfileService.UpdateUserAsync(userProfileEntity);
                    return new UpdateUserProfileResponse{
                        
                    };
                }
                catch (Exception ex)
                {
                    return new UpdateUserProfileResponse { ErrorMessage = ex.Message};
                }
            }
            else
            {
                var errrorMessages = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage));
                return new UpdateUserProfileResponse { ErrorMessage = string.Join(Environment.NewLine, errrorMessages), modelState = ModelState };
            }

        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="request">Old and New Passwords</param>
        /// <returns>If the password has been updated</returns>
        [HttpPost]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var userName = User.Identity.Name;
                    var oldPasswordB64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(request.OldPassword));
                    var newPasswordB64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(request.NewPassword));
                    var userProfileEntity = Mapper.Map<UserProfileEntity>(request);
                    userProfileEntity.UserName = userName;
                    userProfileEntity.PassKey = oldPasswordB64String;

                    var result = await _userProfileService.ChangePasswordAsync(userProfileEntity, newPasswordB64String);
                    return new ChangePasswordResponse
                    {
                        
                    };

                }
                catch(Exception)
                {
                    return new ChangePasswordResponse { ErrorMessage = "Unexpected Error" };
                }
                
            }
            else
            {
                var errrorMessages = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage));
                return new ChangePasswordResponse { ErrorMessage = string.Join(Environment.NewLine, errrorMessages)};
            }
        }

    }
}