using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    /// Searches for User by Display Name
    /// </summary>
    /// <param name="searchTerms">partial display name</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("SearchUserByDisplayName")]
    public async Task<ActionResult<IEnumerable<SearchUserByDisplayNameResponseViewModel>>> SearchUserByDisplayName(SearchUserByDisplayNameRequestViewModel searchTerms)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(new SearchUserByDisplayNameQuery
            {
                CurrentUserName = searchTerms.CurrentUserName,
                QueryPart = searchTerms.SearchPart
            }).ConfigureAwait(false);

            return Ok(response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
    }

    public async Task<ActionResult<SearchUserByUserNameResponseViewModel>> SearchUserByUserName(SearchUserByUserNameResponseViewModel searchTerms)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(Mapper.Map<SearchUserByUserNameQuery>(searchTerms)).ConfigureAwait(false);

            return Ok(new SearchUserByUserNameResponseViewModel
            {
                User  = Mapper.Map<UserProfileViewModel>(response)
            });

        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
