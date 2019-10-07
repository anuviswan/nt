using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nt.WebApi.Interfaces;

namespace Nt.WebApi.Models
{
    public class BaseEntity:IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
