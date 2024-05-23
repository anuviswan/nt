using AuthService.Api.Authentication;
using AuthService.Api.ViewModels.ChangePassword;
using AuthService.Api.ViewModels.Validate;
using AuthService.Domain.Entities;
using AuthService.Service.Command;
using AuthService.Service.Exceptions;
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
    private readonly ILogger<AuthenticationController> _logger;
    public AuthenticationController(IMapper mapper,IMediator mediator,ITokenGenerator tokenService,ILogger<AuthenticationController> logger)
    {
        (_mediator, _mapper, _tokenService, _logger) = (mediator, mapper, tokenService, logger);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("Validate")]
    public async Task<ActionResult<AuthorizeResponseViewModel>> Validate(AuthorizeRequestViewModel request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRequestQuery = _mapper.Map<User>(request);
            var user = await _mediator.Send(new ValidateUserQuery
            {
                User = userRequestQuery
            }).ConfigureAwait(false);


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
            _logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("ChangePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestViewModel request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var changePasswordRequest = _mapper.Map<ChangePasswordCommand>(request);
            changePasswordRequest.UserName = User.Identity!.Name!;
            var response = await _mediator.Send(changePasswordRequest).ConfigureAwait(false);

            if (response) return NoContent();

            return BadRequest("Error changing password. Try again");
        }
        catch (IncorrectPasswordException)
        {
            return BadRequest("Incorrect old password");
        }
        catch (Exception)
        {
            return BadRequest("Error changing password");
        }
    }


}
