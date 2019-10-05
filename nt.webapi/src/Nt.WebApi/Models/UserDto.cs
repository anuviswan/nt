using MongoDB.Bson.Serialization.Attributes;
using Nt.WebApi.Interfaces;

namespace Nt.WebApi.Models
{
    public class UserDto :BaseDto,IErrorInfo
    {
        [BsonElement("userName")]
        public string UserName { get; set; }
        [BsonElement("passKey")]
        public string PassKey { get; set; }
        [BsonElement("displayName")]
        public string DisplayName { get; set; }
        [BsonIgnore]
        public string ErrorMessage { get; set; }
    }
}
