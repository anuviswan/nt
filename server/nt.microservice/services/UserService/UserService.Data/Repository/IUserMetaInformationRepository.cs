namespace UserService.Data.Repository;
public interface IUserMetaInformationRepository:IGenericRepository<UserMetaInformation>
{
    Task<UserMetaInformation> GetUser(string username);
}
