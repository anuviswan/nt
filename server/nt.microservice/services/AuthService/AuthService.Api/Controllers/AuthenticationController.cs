using AuthService.Api.Authentication;
using AuthService.Api.ViewModels.Validate;
using AuthService.Domain.Entities;
using AuthService.Service.Query;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace AuthService.Api.Controllers;
[ApiController]
[Route("api/authenticate")]
public class AuthenticationController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ITokenGenerator _tokenService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationController> _logger;
    public AuthenticationController(IMapper mapper,IMediator mediator,ITokenGenerator tokenService,IConfiguration configuration, ILogger<AuthenticationController> logger)
    {
        (_mediator, _mapper, _tokenService, _configuration, _logger) = (mediator, mapper, tokenService, configuration,logger);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("Validate")]
    public async Task<ActionResult<AuthorizeResponseViewModel>> Validate(AuthorizeRequestViewModel request)
    {
        try
        {
            
            var userRequestQuery = _mapper.Map<User>(request);
            var user = await _mediator.Send(new ValidateUserQuery
            {
                User = userRequestQuery
            });


            if(user is null)
            {
                _logger.LogInformation("Invalid User");
                return Unauthorized();
            }

            var generatedToken = _tokenService.Generate(user.UserName);

            var response = _mapper.Map<AuthorizeResponseViewModel>(user);
            response.IsAuthenticated = true;
            response.Token = generatedToken;

            _logger.LogInformation("Valid User");
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
