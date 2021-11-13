using MongoDB.Bson;

namespace Nt.Infrastructure.Data.Repositories.User;
public class UserProfileRepository : GenericRepository<UserProfileEntity>, IUserProfileRepository
{
    public UserProfileRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
            
    }

    public override async Task<bool> UpdateAsync(UserProfileEntity data)
    {
        var filter = Builders<BsonDocument>.Filter.Eq(data.Id, data.Id);
        var update = Builders<UserProfileEntity>.Update
            .Set(x => x.Bio, data.Bio)
            .Set(x => x.ChangedOn, DateTime.UtcNow)
            .Set(x => x.DisplayName, data.DisplayName)
            .Set(x => x.IsDeleted, data.IsDeleted);
        var result = await _dataCollection.UpdateOneAsync<UserProfileEntity>(x=>x.UserName.Equals(data.UserName), update);
        return result.ModifiedCount == 1;
    }

}
