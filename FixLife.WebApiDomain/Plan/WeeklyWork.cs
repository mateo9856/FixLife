using FixLife.WebApiDomain.Common;
using FixLife.WebApiDomain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace FixLife.WebApiDomain.Plan
{
    public class WeeklyWork : BaseBusinessEntity
    {
        [BsonElement("timeStart")]
        public TimeSpan TimeStart { get; set; }
        [BsonElement("timeEnd")]
        public TimeSpan TimeEnd { get; set; }
        [BsonElement("dayOfWeeks")]
        public ICollection<DayOfWeeks> DayOfWeeks { get; set; }
    }
}
