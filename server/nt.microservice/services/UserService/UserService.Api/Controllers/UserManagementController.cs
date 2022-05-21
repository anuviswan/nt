using Microsoft.AspNetCore.Mvc;
using UserService.Api.ViewModels.UserManagement;

namespace UserService.Api.Controllers;

[ApiController]
[Route("Users")]
public class UserManagementController : Controller
{
    public Action<IEnumerable<SearchUserResponseViewModel>> SearchUser(SearchUserRequestViewModel searchTerms)
    {
        return default;
    }
}
