using AuthService.Data.Database;
using AuthService.Domain.Entities;

namespace AuthService.Data.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(UserDbContext userDbContext) : base(userDbContext)
    {

    }
    public Task<bool> ValidateUser(User user)
    {
        var isValid = UserDbContext.Set<User>().Any(x => string.Equals(x.UserName, user.UserName, StringComparison.OrdinalIgnoreCase) &&
                                            string.Equals(x.Password, user.Password));
        return Task.FromResult(isValid);
    }
}
