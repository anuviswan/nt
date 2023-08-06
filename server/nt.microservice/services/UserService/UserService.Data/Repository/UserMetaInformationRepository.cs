using Microsoft.EntityFrameworkCore;

namespace UserService.Data.Repository;
public class UserMetaInformationRepository : GenericRepository<UserMetaInformation,UserserviceDbContext>, IUserMetaInformationRepository
{
    public UserMetaInformationRepository(UserserviceDbContext userDbContext) : base(userDbContext)
    {
    }

    public async Task<UserMetaInformation?> GetUser(string username)
    {
        return await DatabaseContext.UserMetaInformation
            .FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower()).ConfigureAwait(false);
    }


    public async Task<IEnumerable<UserMetaInformation>> SearchUserByPartialDisplayName(string searchTerm)
    {
        var results = await DatabaseContext.UserMetaInformation.Where(x=> x.DisplayName != null && x.DisplayName.StartsWith(searchTerm)).ToListAsync().ConfigureAwait(false);
        return results;
    } 
}
