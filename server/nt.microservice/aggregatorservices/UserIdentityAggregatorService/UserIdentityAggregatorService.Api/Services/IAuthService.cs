
namespace UserIdentityAggregatorService.Api.Services;

public interface IAuthService
{
    Task<AuthenticateResponseViewModel?> ValidateAsync(AuthorizeRequestViewModel request);
}