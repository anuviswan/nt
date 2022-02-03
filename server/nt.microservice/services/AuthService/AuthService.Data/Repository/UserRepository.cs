using AuthService.Data.Database;
using AuthService.Domain.Entities;

namespace AuthService.Data.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(IUnitOfWork unitOfWor) : base(userDbContext)
    {

    }
    public Task<User> ValidateUser(User user)
    {
        var userFound = UserDbContext.Set<User>().FirstOrDefault(x => x.UserName.ToLower().Equals(user.UserName.ToLower()) &&
                                            x.Password.ToLower().Equals(user.Password.ToLower()));
        return Task.FromResult(userFound);
    }
}
