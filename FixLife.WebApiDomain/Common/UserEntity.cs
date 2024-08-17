using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixLife.WebApiDomain.Common
{
    public class UserEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }
        //public string[] Tags { get; set; }

    }
}
