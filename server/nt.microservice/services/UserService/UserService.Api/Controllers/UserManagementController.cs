﻿using Microsoft.AspNetCore.Mvc;
using UserService.Api.ViewModels.UserManagement;
using UserService.Service.Query;

namespace UserService.Api.Controllers;

[ApiController]
[Route("UserManagement")]
public class UserManagementController : BaseController
{
    public UserManagementController(IMediator mediator, IMapper mapper, ILogger<UserManagementController> logger) : base(mediator,mapper,logger)
    {
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("SearchUserByDisplayName")]
    public ActionResult<IEnumerable<SearchUserResponseViewModel>> SearchUserByDisplayName(SearchUserRequestViewModel searchTerms)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = Mediator.Send(new SearchUserByDisplayNameQuery
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
