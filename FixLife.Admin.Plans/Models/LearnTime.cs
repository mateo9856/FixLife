using FixLife.Admin.Plans.Enums;

namespace FixLife.Admin.Plans.Models
{
    public record LearnTime
    {
        public Guid Id { get; set; }
        public TimeSpan TimeInterval { get; set; }
        public TimeSpan StartTime { get; set; }
        public ICollection<DayOfWeeks> DayOfWeeks { get; set; }
    }
}
