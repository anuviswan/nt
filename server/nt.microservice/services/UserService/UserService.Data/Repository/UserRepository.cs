namespace UserService.Data.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(UserManagementDbContext userDbContext) : base(userDbContext)
    {

    }
}
