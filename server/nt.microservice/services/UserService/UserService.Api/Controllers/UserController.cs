using MassTransit;
using nt.shared.dto.User;
using UserService.Api.ViewModels.User;
using UserService.Domain.Entities;

namespace UserService.Api.Controllers;

[ApiController]
[Route("Users")]
public class UserController : BaseController
{
    private readonly IPublishEndpoint  _publishEndPoint;
    public UserController(IMediator mediator,IMapper mapper, ILogger<UserController> logger, IPublishEndpoint publishEndPoint):base(mediator,mapper,logger)
    {
        _publishEndPoint = publishEndPoint;
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

            //var result = await Mediator.Send(new CreateUserCommand
            //{
            //    UserProfile = userProfileToCreate,
            //    UserCredential = userCredentialToCreate
            //});

            await _publishEndPoint.Publish<CreateUserDto>(new()
            {
                UserName = userCredentialToCreate.UserName,
                Password = userCredentialToCreate.Password
            });
            return new OkObjectResult(Mapper.Map<CreateUserResponseViewModel>(null));
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
