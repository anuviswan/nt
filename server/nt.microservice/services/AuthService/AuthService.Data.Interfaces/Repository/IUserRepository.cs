using AuthService.Data.Interfaces.Entities;
namespace AuthService.Data.Interfaces.Repository;
public interface IUserRepository:IGenericRepository<User>
{
    Task<User?> ValidateUserAsync(User user);
    Task<bool> ChangePasswordAsync(User user,string newPassword);
}