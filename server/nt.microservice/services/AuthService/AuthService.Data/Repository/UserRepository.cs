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
        var isValid = UserDbContext.Set<User>().Any(x => x.UserName.ToLower().Equals(user.UserName.ToLower()) &&
                                            x.Password.ToLower().Equals(user.Password.ToLower()));
        return Task.FromResult(isValid);
    }
}
