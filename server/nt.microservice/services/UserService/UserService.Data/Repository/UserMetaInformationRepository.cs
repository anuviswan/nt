namespace UserService.Data.Repository;
public class UserMetaInformationRepository : GenericRepository<UserMetaInformation>, IUserMetaInformationRepository
{
    public UserMetaInformationRepository(UserManagementDbContext userDbContext) : base(userDbContext)
    {
    }
}
