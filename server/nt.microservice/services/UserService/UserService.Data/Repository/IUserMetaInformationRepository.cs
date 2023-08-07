namespace UserService.Data.Repository;
public interface IUserMetaInformationRepository:IGenericRepository<UserMetaInformation,UserserviceDbContext>
{
    Task<UserMetaInformation?> GetUser(string username);
    Task<IEnumerable<UserMetaInformation>> SearchUserByPartialDisplayName(string searchTerm);
}
