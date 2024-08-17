using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixLife.WebApiDomain.Common
{
    public class BaseBusinessEntity : IAudible
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("createdDate")]
        public DateTime? CreatedDate { get; set; }
        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }
        [BsonElement("deletedDate")]
        public DateTime? DeletedDate { get; set; }

        public BaseBusinessEntity()
        {
            Id = ObjectId.GenerateNewId();
            CreatedDate = DateTime.Now;
        }

    }
}
