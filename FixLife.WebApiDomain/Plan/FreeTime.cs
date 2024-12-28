using FixLife.WebApiDomain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace FixLife.WebApiDomain.Plan
{
    public class FreeTime : BaseBusinessEntity
    {
        [BsonElement("timeStart")]
        public TimeSpan TimeStart { get; set; }
        [BsonElement("timeEnd")]
        public TimeSpan TimeEnd { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
    }
}
