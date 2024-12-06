using FixLife.WebApiDomain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixLife.WebApiDomain.Plan
{
    public class Plan : BaseBusinessEntity
    {
        [BsonElement("freeTimeId")]
        public List<ObjectId> FreeTimeId { get; set; }
        [BsonElement("learnTimeId")]
        public ObjectId LearnTimeId { get; set; }
        [BsonElement("weeklyWork")]
        public ObjectId WeeklyWorkId { get; set; }
        [BsonElement("userId")]
        public ObjectId UserId { get; set; }
    }
}
