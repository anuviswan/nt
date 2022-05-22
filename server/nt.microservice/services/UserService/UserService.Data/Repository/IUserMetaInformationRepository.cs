namespace UserService.Data.Repository;
public interface IUserMetaInformationRepository:IGenericRepository<UserMetaInformation>
{
    Task<UserMetaInformation> GetUser(string username);
    Task<IEnumerable<UserMetaInformation>> SearchUser(string searchTerm);
}
