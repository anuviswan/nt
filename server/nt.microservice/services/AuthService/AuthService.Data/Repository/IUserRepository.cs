using AuthService.Domain.Entities;

namespace AuthService.Data.Repository;
public interface IUserRepository:IGenericRepository<User>
{
    Task<bool> ValidateUser(User user);
}