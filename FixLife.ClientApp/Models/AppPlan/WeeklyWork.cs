using FixLife.ClientApp.Common.Enums;

namespace FixLife.ClientApp.Models
{
    public class WeeklyWork
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public List<DayOfTheWeekEnum> DayOfWeeks { get; set; }

    }
}
