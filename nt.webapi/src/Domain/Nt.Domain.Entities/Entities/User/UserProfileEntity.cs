using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;

namespace Nt.Domain.Entities.User
{
    [BsonCollection("Users")]
    public record UserProfileEntity : BaseEntity
    {
        [BsonElement("userName")]
        public string UserName { get; init; }
        [BsonElement("passKey")]
        public string PassKey { get; init; }
        [BsonElement("displayName")]
        public string DisplayName { get; init; }

        [BsonElement("bio")]
        public string Bio { get; init; }

        [BsonElement("isDeleted")]
        [BsonDefaultValue(false)]
        public bool IsDeleted { get; init; } 

        [BsonCollection("followers")]
        public IEnumerable<string> Followers { get; init; }

        [BsonCollection("follows")]
        public IEnumerable<string> Follows { get; init; }

    }
}
