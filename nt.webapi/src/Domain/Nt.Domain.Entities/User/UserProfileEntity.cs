using MongoDB.Bson.Serialization.Attributes;
using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;

namespace Nt.Domain.Entities.User
{
    [BsonCollection("UserProfile")]
    public class UserProfileEntity : BaseEntity
    {
        [BsonElement("userName")]
        public string UserName { get; set; }
        [BsonElement("passKey")]
        public string PassKey { get; set; }
        [BsonElement("displayName")]
        public string DisplayName { get; set; }

    }
}
