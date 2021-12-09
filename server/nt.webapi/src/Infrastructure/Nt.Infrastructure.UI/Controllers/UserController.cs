using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.WebApi.Authentication;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ChangePassword;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.FollowUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.UnfollowUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.UpdateUser;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser;

namespace Nt.Infrastructure.WebApi.Controllers;

/// <summary>
/// Exposes API End points for User Creation and Maintaince
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    private readonly IUserProfileService _userProfileService;
    private readonly IUserManagementService _userManagementService;
    private readonly ITokenGenerator _tokenGenerator;

    private const string GetAll_RouteName = "GetAllUsers";
    private const string SearchUser_RouteName = "SearchUser";
    private const string GetUser_RouteName = "GetUser";
    private const string ValidateUser_RouteName = "ValidateUser";
    private const string CreateUser_RouteName = "CreateUser";
    private const string UpdateUser_RouteName = "UpdateUser";
    private const string ChangePassword_RouteName = "ChangePassword";
    private const string FollowUser_RouteName = "FollowUser";

    public UserController(IMapper mapper, IUserProfileService userProfileService,IUserManagementService userManagementService,ITokenGenerator tokenGenerator) : base(mapper)
    {
        (_userProfileService, _userManagementService,_tokenGenerator) = (userProfileService, userManagementService,tokenGenerator);
    }

    /// <summary>
    /// Get all Users in the system
    /// </summary>
    /// <returns>User List</returns>
    [HttpGet]
    [Route(GetAll_RouteName)]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserProfileResponse>>> GetAll()
    {
        var usersFound = await _userManagementService.GetAllUsersAsync();
        return Ok(Mapper.Map<IEnumerable<UserProfileResponse>>(usersFound));
    }

    /// <summary>
    /// Search for users
    /// </summary>
    /// <param name="partialString">Partial username to be searched</param>
    /// <returns>Collection of Users who matches partial user name</returns>
    [HttpGet]
    [Route(SearchUser_RouteName)]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserProfileResponse>>> SearchUser(string partialString)
    {
        var usersFound = await _userManagementService.SearchUserAsync(partialString);
        return Ok(Mapper.Map<IEnumerable<UserProfileResponse>>(usersFound));
    }


    /// <summary>
    /// Get User based on UserName
    /// </summary>
    /// <param name="userName">Username to search for</param>
    /// <returns>Returns User Profile if Single User found. Throws exception otherwise.</returns>
    [HttpGet]
    [Route(GetUser_RouteName)]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserProfileResponse>> GetUser(string userName)
    {
        try
        {
            var userFound = await _userManagementService.GetUserAsync(userName);
            return Ok(Mapper.Map<UserProfileResponse>(userFound));
        }
        catch(EntityNotFoundException)
        {
            return BadRequest("User Not Found");
        }
           
    }

    /// <summary>
    /// Validate User Login Request
    /// </summary>
    /// <param name="loginRequest">Includes UserName and Base 64 encoded password</param>
    /// <returns>User Details if successfull login with IsAuthenticated Flag true. Invalid User with IsAuthenticated Flag false if validation fails</returns>
    [HttpPost]
    [AllowAnonymous]
    [Route(ValidateUser_RouteName)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> ValidateUser(LoginRequest loginRequest)
    {
        try
        {
            UserProfileEntity userEntity = Mapper.Map<UserProfileEntity>(loginRequest);
            var validUser = await _userProfileService.AuthenticateAsync(userEntity);
            var tokenString = _tokenGenerator.Generate(validUser);
            var result = Mapper.Map<LoginResponse>(validUser);
            var resultWithToken = result with
            {
                Token = tokenString,
                IsAuthenticated = true,
                LoginTime = DateTime.UtcNow
            };
            return Ok(resultWithToken);
        }
        catch(InvalidPasswordOrUsernameException)
        {
            return BadRequest("Invalid Password or Username");
        }
           
           
    }

    /// <summary>
    /// Creates a new User with the specified details
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Returns User details if User is created sucessfully. Returns token with Error Message if User already exists with same username</returns>
    [HttpPost]
    [Route(CreateUser_RouteName)]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateUserProfileResponse>> CreateUser(CreateUserProfileRequest user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userEntity = Mapper.Map<UserProfileEntity>(user);

        var formattedUser = userEntity with
        {
            UserName = userEntity.UserName.ToLower(),
            PassKey = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(user.PassKey))
        };

        try
        {
            var result = await _userProfileService.CreateUserAsync(formattedUser);
            return Ok(Mapper.Map<CreateUserProfileResponse>(result));
        }
        catch (UserNameExistsException)
        {
            return BadRequest("User Name already exists");
        }

           
    }

    /// <summary>
    /// Update User Profile
    /// </summary>
    /// <param name="user">User Profile to update</param>
    /// <returns>Updated User Profile</returns>
    [HttpPost]
    [Route(UpdateUser_RouteName)]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(UpdateUserProfileRequest user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userName = User.Identity.Name;
        var userProfileEntity = Mapper.Map<UserProfileEntity>(user) with
        {
            UserName = userName
        };

        var _ = await _userProfileService.UpdateUserAsync(userProfileEntity);
        return NoContent();
    }

    /// <summary>
    /// Change Password
    /// </summary>
    /// <param name="request">Old and New Passwords</param>
    /// <returns>If the password has been updated</returns>
    [HttpPost]
    [Route(ChangePassword_RouteName)]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userName = User.Identity.Name;
            var oldPasswordB64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(request.OldPassword));
            var newPasswordB64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(request.NewPassword));
            var userProfileEntity = Mapper.Map<UserProfileEntity>(request) with
            {
                UserName = userName,
                PassKey = oldPasswordB64String
            };

            var _ = await _userProfileService.ChangePasswordAsync(userProfileEntity, newPasswordB64String);
            return NoContent();
        }
        catch (InvalidPasswordOrUsernameException)
        {
            return BadRequest("Incorrect old password");
        }
    }


    /// <summary>
    /// Mark User to Follow
    /// </summary>
    /// <param name="request">User to Follow</param>
    /// <returns></returns>
    [HttpPost]
    [Route(FollowUser_RouteName)]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FollowUser(FollowUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var currentUser = User.Identity.Name;
            await _userManagementService.FollowUserAsync(currentUser, request.UserToFollow);
            return NoContent();
        }
        catch (EntityNotFoundException)
        {

            return BadRequest("User not found");
        }
    }


    public async Task<IActionResult> UnfollowUser(UnfollowUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var currentUser = User.Identity.Name;
            await _userManagementService.UnfollowUserAsync(currentUser, request.UserToUnfollow);
            return NoContent();
        }
        catch (EntityNotFoundException)
        {

            return BadRequest("User not found");
        }
    }

}
