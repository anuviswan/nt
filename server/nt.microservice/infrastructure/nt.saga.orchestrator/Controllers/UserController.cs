using Microsoft.AspNetCore.Mvc;
using nt.saga.orchestrator.ViewModels.ValidateUser;
using Nt.Saga.Orchestrator.Services.AuthService;
using Nt.Saga.Orchestrator.Services.UserService;

namespace nt.saga.orchestrator.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<ValidateUserResponseViewModel>> ValidateUser(ValidateUserRequestViewModel request)
        {
            var authenticateResponse = await _authService.ValidateAsync(new AuthorizeRequestViewModel
            {
                UserName = request.userName,
                PassKey = request.passKey
            });

            return default;
        }
    }
}