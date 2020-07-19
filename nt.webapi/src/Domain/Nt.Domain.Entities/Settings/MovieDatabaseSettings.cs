namespace Nt.Domain.Entities.Settings
{
    public class MovieDatabaseSettings : IMovieDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }


    public interface IMovieDatabaseSettings : IDatabaseSettings
    {

    }

}
