using Microsoft.EntityFrameworkCore;

namespace UserService.Data.Repository;
public class UserMetaInformationRepository : GenericRepository<UserMetaInformation>, IUserMetaInformationRepository
{
    public UserMetaInformationRepository(UserManagementDbContext userDbContext) : base(userDbContext)
    {
    }

    public async Task<UserMetaInformation> GetUser(string username)
    {
        return await UserDbContext.UsersInfo
            .Include(x=>x.User)
            .FirstOrDefaultAsync(x => x.User.UserName.ToLower() == username.ToLower());
    }


    public async Task<IEnumerable<UserMetaInformation>> SearchUser(string searchTerm)
    {
        var results = await UserDbContext.UsersInfo.Where(x=>x.DisplayName.StartsWith(searchTerm)).ToListAsync();
        return results;
    } 
}
