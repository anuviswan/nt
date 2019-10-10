using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nt.WebApi.Shared.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
