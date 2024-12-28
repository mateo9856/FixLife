using FixLife.ClientApp.Common.Enums;

namespace FixLife.ClientApp.Models
{
    public class LearnTime
    {
        public TimeSpan TimeInterval { get; set; }
        public TimeSpan StartTime { get; set; }
        public List<DayOfTheWeekEnum> DayOfWeeks { get; set; }

    }
}
