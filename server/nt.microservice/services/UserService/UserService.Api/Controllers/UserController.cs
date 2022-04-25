using Microsoft.AspNetCore.Authorization;
using UserService.Api.ViewModels.User;
using UserService.Domain.Entities;

namespace UserService.Api.Controllers;

[ApiController]
[Route("Users")]
public class UserController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    public UserController(IMediator mediator,IMapper mapper, ILogger<UserController> logger)
    {
        _logger = logger;
        (_mediator,_mapper) = (mediator,mapper);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("Register")]
    public async Task<ActionResult<CreateUserResponseViewModel>> RegisterUser(CreateUserRequestViewModel user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = _mapper.Map<UserMetaInformation>(user);
            var result = await _mediator.Send(new CreateUserCommand
            {
                User = userToCreate,
            });
            return new OkObjectResult(_mapper.Map<CreateUserResponseViewModel>(result));
        }
        catch(Exception ex)
        {
            _logger.LogError($"Error registering user : {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("ChangePassword")]
    [Authorize]
    public async Task<ActionResult<ChangePasswordResponseViewModel>> ChangePassword(ChangePasswordRequestViewModel request)
    {

        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToUpdate = _mapper.Map<User>(request);
            var response = await _mediator.Send(new ChangePasswordCommand
            {
                UserToUpdate = userToUpdate
            });

            
            return new OkObjectResult(new ChangePasswordResponseViewModel
            {
                UserName = request.UserName,
                IsPasswordChanged = response,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Changing Password : {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("DemoMethod")]
    public async Task<ActionResult<string>> DemoMethod()
    {
        _logger.LogError("Forced Error");
        return new OkObjectResult("Random");
    }

}
