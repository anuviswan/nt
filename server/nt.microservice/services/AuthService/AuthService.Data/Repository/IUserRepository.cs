using AuthService.Domain.Entities;

namespace AuthService.Data.Repository;
public interface IUserRepository:IGenericRepository<User>
{
    Task<User> ValidateUser(User user);
}