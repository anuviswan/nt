using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UserService.Api.ViewModels.UserManagement;
using UserService.Service.Query;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/UserManagement")]
public class UserManagementController : BaseController
{
    public UserManagementController(IMediator mediator, IMapper mapper, ILogger<UserManagementController> logger) : base(mediator,mapper,logger)
    {
    }

    /// <summary>
    /// Searches for User by Display Name
    /// </summary>
    /// <param name="searchTerms">partial display name</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("SearchUserByDisplayName")]
    public async Task<ActionResult<IEnumerable<SearchUserByDisplayNameResponseViewModel>>> SearchUserByDisplayName([FromQuery]string searchTerm)
    {
        try
        {
            if (string.IsNullOrEmpty(searchTerm)) return BadRequest("Search Term cannot be empty.");

            if (User?.Identity?.Name == null) return BadRequest("Invalid User");

            var response = await Mediator.Send(new SearchUserByDisplayNameQuery
            {
                CurrentUserName = User.Identity.Name,
                QueryPart = searchTerm
            }).ConfigureAwait(false);

            return Ok(Mapper.Map<SearchUserByDisplayNameResponseViewModel>(response));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("SearchUserByUserName/{userName}")]
    public async Task<ActionResult<SearchUserByUserNameResponseViewModel>> SearchUserByUserName([FromRoute][Required][MinLength(1)] string userName)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                ModelState.AddModelError(nameof(userName), "UserName is mandatory.");
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(Mapper.Map<SearchUserByUserNameQuery>(userName)).ConfigureAwait(false);

            return Ok(Mapper.Map<SearchUserByUserNameResponseViewModel>(response));

        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
