using MongoDB.Bson.Serialization.Attributes;

namespace Nt.WebApi.Shared.Entities
{
    public class UserEntity : BaseEntity
    {
        [BsonElement("userName")]
        public string UserName { get; set; }
        [BsonElement("passKey")]
        public string PassKey { get; set; }
        [BsonElement("displayName")]
        public string DisplayName { get; set; }

    }
}
