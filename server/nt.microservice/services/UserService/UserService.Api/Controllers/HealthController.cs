
namespace UserService.Api.Controllers;

[ApiController]
[Route("api/healthcheck")]
public class HealthController : BaseController
{
    public HealthController(IMediator mediator, IMapper mapper, ILogger<HealthController> logger) : base(mediator, mapper, logger)
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("health")]
    public ActionResult Health()
    {
        return Ok();
    }
}
