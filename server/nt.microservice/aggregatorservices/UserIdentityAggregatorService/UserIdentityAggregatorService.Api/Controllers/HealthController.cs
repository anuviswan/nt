using Microsoft.AspNetCore.Mvc;

namespace UserIdentityAggregatorService.Api.Controllers;

[ApiController]
[Route("api/healthcheck")]
public class HealthController : ControllerBase
{
    public HealthController(ILogger<HealthController> logger) 
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