using FixLife.WebApiDomain.Common;

namespace FixLife.WebApiDomain.Plan
{
    public class LearnTime : BaseBusinessEntity
    {
        public TimeSpan TimeInterval { get; set; }
        public TimeSpan StartTime { get; set; }
        public ICollection<Common.DayOfWeek> DayOfWeeks { get; set; }
    }
}
