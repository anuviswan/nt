using Microsoft.AspNetCore.Authorization;
using UserService.Api.ViewModels.User;
using UserService.Domain.Entities;

namespace UserService.Api.Controllers;

[ApiController]
[Route("Users")]
public class UserController : BaseController
{
    public UserController(IMediator mediator,IMapper mapper, ILogger<UserController> logger):base(mediator,mapper,logger)
    {
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

            var userProfileToCreate = Mapper.Map<UserMetaInformation>(user);
            var userCredentialToCreate = Mapper.Map<User>(user);

            var result = await Mediator.Send(new CreateUserCommand
            {
                UserProfile = userProfileToCreate,
                UserCredential = userCredentialToCreate
            });
            return new OkObjectResult(Mapper.Map<CreateUserResponseViewModel>(result));
        }
        catch(Exception ex)
        {
            Logger.LogError($"Error registering user : {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("DemoMethod")]
    public async Task<ActionResult<string>> DemoMethod()
    {
        Logger.LogError("Forced Error");
        return new OkObjectResult("Random");
    }

}
