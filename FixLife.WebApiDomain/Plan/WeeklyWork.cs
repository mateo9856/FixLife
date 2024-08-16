using FixLife.WebApiDomain.Common;

namespace FixLife.WebApiDomain.Plan
{
    public class WeeklyWork : BaseBusinessEntity
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public ICollection<Common.DayOfWeek> DayOfWeeks { get; set; }
    }
}
