using MongoDB.Bson.Serialization.Attributes;
using Nt.WebApi.Interfaces;

namespace Nt.WebApi.Models
{
    public class UserEntity :BaseEntity
    {
        [BsonElement("userName")]
        public string UserName { get; set; }
        [BsonElement("passKey")]
        public string PassKey { get; set; }
        [BsonElement("displayName")]
        public string DisplayName { get; set; }

    }
}
