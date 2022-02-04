using AuthService.Data.Database;
using AuthService.Domain.Entities;
using System.Data;

namespace AuthService.Data.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(IDbConnection dbConnection) : base(dbConnection)
    {

    }
    public async Task<User> ValidateUser(User user)
    {
        var users = await GetAll(); 
        var userFound = users.FirstOrDefault(x => x.UserName.ToLower().Equals(user.UserName.ToLower()) &&
                                            x.Password.ToLower().Equals(user.Password.ToLower()));
        return userFound;
    }
}
