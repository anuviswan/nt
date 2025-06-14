using Microsoft.AspNetCore.Mvc;

namespace MovieService.Api.Controllers;
[ApiController]
[Route("api/healthcheck")]
public class HealthController : BaseController
{
    public HealthController(ILogger<HealthController> logger) : base(logger)
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