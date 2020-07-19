using System;

namespace Nt.Domain.Entities.Attributes
{
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; set; }
        public BsonCollectionAttribute(string collectionName) => CollectionName = collectionName;
    }
}
