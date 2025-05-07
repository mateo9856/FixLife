using FixLife.Admin.Plans.Enums;

namespace FixLife.Admin.Plans.Models
{
    public record WeeklyWork
    {
        public Guid Id { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public ICollection<DayOfWeeks> DayOfWeeks { get; set; }
    }
}
