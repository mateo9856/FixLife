using FixLife.WebApiDomain.Common;
using FixLife.WebApiDomain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace FixLife.WebApiDomain.Plan
{
    public class LearnTime : BaseBusinessEntity
    {
        [BsonElement("timeInterval")]
        public TimeSpan TimeInterval { get; set; }
        [BsonElement("startTime")]
        public TimeSpan StartTime { get; set; }
        [BsonElement("dayOfWeeks")]
        public ICollection<DayOfWeeks> DayOfWeeks { get; set; }
    }
}
