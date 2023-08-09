using Microsoft.AspNetCore.Mvc;
using nt.saga.orchestrator.ViewModels.ValidateUser;
using Nt.Saga.Orchestrator.Services.AuthService;
using Nt.Saga.Orchestrator.Services.UserService;

namespace nt.saga.orchestrator.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, AuthService authService, ILogger<UserController> logger)
        {
            _userService = userService;
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("Validate")]
        public async Task<ActionResult<ValidateUserResponseViewModel>> ValidateUser(ValidateUserRequestViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var authenticateResponse = await _authService.ValidateAsync(new AuthorizeRequestViewModel
                {
                    UserName = request.userName,
                    PassKey = request.passKey
                });

                if (!authenticateResponse.IsAuthenticated)
                    return Unauthorized();

                var userDetails = await _userService.SearchUserByUserNameAsync(new SearchUserByUserNameRequestViewModel { UserName = request.userName });

                return Ok(new ValidateUserResponseViewModel
                {
                    IsAuthenticated = true,
                    Token = authenticateResponse.Token,
                    LoginTime = authenticateResponse.LoginTime,
                    UserName = authenticateResponse.UserName,
                    Bio = userDetails.User.Bio,
                    DisplayName = userDetails.User.DisplayName,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}