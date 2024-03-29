﻿using MongoDB.Bson;

namespace Nt.Domain.Entities.Entities;
public record BaseEntity : IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; }
    public DateTime CreatedOn { get; init; }
    public DateTime ChangedOn { get; init; }
}
