using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Nt.Domain.Entities.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ChangedOn { get; set; }
    }
}
