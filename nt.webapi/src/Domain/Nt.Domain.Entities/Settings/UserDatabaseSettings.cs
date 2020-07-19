namespace Nt.Domain.Entities.Settings
{
    public class UserDatabaseSettings : IUserDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }


    public interface IUserDatabaseSettings : IDatabaseSettings
    {

    }

}
