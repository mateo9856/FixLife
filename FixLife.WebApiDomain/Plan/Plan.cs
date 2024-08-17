using FixLife.WebApiDomain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace FixLife.WebApiDomain.Plan
{
    public class Plan : BaseBusinessEntity
    {
        [BsonElement("freeTime")]
        public ICollection<FreeTime> FreeTime { get; set; }
        [BsonElement("learnTime")]
        public LearnTime LearnTime { get; set; }
        [BsonElement("weeklyWork")]
        public WeeklyWork WeeklyWork { get; set; }
        [BsonElement("userId")]
        public string UserId { get; set; }
    }
}
