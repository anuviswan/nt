﻿using AuthService.Api.ViewModels.AddUser;
using AuthService.Domain.Entities;
using AuthService.Service.Command;
using AuthService.Service.Helpers.Exceptions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;



namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public UserController(IMapper mapper,IMediator mediator)
    {
        (_mapper,_mediator)  = (mapper, mediator);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AddUserResponseViewModel>> AddUserAsync(AddUserRequestViewModel model)
    {
        try
        {
            var userModel = _mapper.Map<User>(model);
            userModel.Id = Guid.NewGuid();

            var result = await _mediator.Send(new AddUserCommand
            {
                User = userModel,
            });

            var response = _mapper.Map<AddUserResponseViewModel>(result);
            return Ok(response);

        }
        catch (UserAlreadyExistsException)
        {
            return BadRequest("User already exists");
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}