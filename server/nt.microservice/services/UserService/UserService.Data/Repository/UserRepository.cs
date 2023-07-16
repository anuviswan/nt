namespace UserService.Data.Repository;
public class UserRepository : GenericRepository<User,UserManagementDbContext>, IUserRepository
{
    public UserRepository(UserManagementDbContext userDbContext) : base(userDbContext)
    {

    }
}
