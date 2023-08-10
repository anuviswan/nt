using AuthService.Domain.Entities;

namespace AuthService.Data.Repository;
public interface IUserRepository:IGenericRepository<User>
{
    Task<User?> ValidateUserAsync(User user);
    Task<bool> ChangePasswordAsync(User user,string newPassword);
}