﻿using AuthService.Api.ViewModels.Validate;
using AuthService.Domain.Entities;
using AuthService.Service.Query;
using MapsterMapper;
using MediatR;

namespace AuthService.Api.Controllers;
[ApiController]
[Route("api/authenticate")]
public class AuthenticationController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public AuthenticationController(IMapper mapper,IMediator mediator)
    {
        (_mediator, _mapper) = (mediator, mapper);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("Validate")]
    public async Task<ActionResult<ValidateResponseViewModel>> Validate(ValidateRequestViewModel request)
    {
        try
        {
            var userRequestQuery = _mapper.Map<User>(request);
            var response = await _mediator.Send(new ValidateUserQuery
            {
                User = userRequestQuery
            });

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
