using AuthService.Data.Interfaces.Repository;
using System.Data;

namespace AuthService.Data.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(IDbConnection dbConnection) : base(dbConnection)
    {

    }
    public async Task<User?> ValidateUserAsync(User user)
    {
        var users = await GetAll(); 
        var userFound = users.FirstOrDefault(x => x.UserName.ToLower().Equals(user.UserName.ToLower()) &&
                                            x.Password.ToLower().Equals(user.Password.ToLower()));
        return userFound;
    }

    public async Task<bool> ChangePasswordAsync(User user, string newPassword)
    {
        user.Password = newPassword;
        await UpdateAsync(user).ConfigureAwait(false);
        return true;

    }
}
