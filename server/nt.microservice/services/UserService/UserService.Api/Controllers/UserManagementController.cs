﻿using Microsoft.AspNetCore.Mvc;
using UserService.Api.ViewModels.UserManagement;
using UserService.Service.Query;

namespace UserService.Api.Controllers;

[ApiController]
[Route("Users")]
public class UserManagementController : BaseController
{
    private IMediator _mediator;
    public UserManagementController(IMediator mediator, IMapper mapper, ILogger logger) : base(mediator,mapper,logger)
    {
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("SearchUser")]
    public ActionResult<IEnumerable<SearchUserResponseViewModel>> SearchUser(SearchUserRequestViewModel searchTerms)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = Mediator.Send(new SearchUserQuery
            {
                CurrentUserName = searchTerms.CurrentUserName,
                QueryPart = searchTerms.SearchPart
            });

            return Ok(response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
