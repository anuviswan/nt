using FluentValidation;

namespace AuthService.Api.ViewModels.Validate;
public class AuthorizeResponseViewModel
{
    public string Token { get; set; }
    public bool IsAuthenticated { get; set; }

    public DateTime LoginTime { get; set; }

    public string UserName { get; set; }
}
