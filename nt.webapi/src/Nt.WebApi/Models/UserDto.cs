using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Models
{
    public class UserDto :BaseDto
    {
        [BsonElement("userName")]
        public string UserName { get; set; }
        [BsonElement("passKey")]
        public string PassKey { get; set; }
    }
}
