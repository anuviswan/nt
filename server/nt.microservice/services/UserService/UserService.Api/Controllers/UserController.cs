using UserService.Api.ViewModels.User;
using UserService.Domain.Entities;

namespace UserService.Api.Controllers;

[ApiController]
[Route("Users")]
public class UserController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public UserController(IMediator mediator,IMapper mapper)
    {
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
            var userToCreate = _mapper.Map<UserMetaInformation>(user);
            var result = await _mediator.Send(new CreateUserCommand
            {
                User = userToCreate,
            });
            return new OkObjectResult(_mapper.Map<CreateUserResponseViewModel>(result));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
