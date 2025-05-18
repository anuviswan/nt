
namespace UserIdentityAggregatorService.Api.Services;

public interface IUserService
{
    Task<SearchUserByUserNameResponseViewModel?> SearchUserByUserNameAsync(string userName);
}