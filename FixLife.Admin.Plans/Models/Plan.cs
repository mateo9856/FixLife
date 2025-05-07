namespace FixLife.Admin.Plans.Models
{
    public record Plan
    {
        public Guid Id { get; set; }

        public WeeklyWork WeeklyWork { get; set; }

        public LearnTime LearnTime { get; set; }

        public FreeTime FreeTime { get; set; }
    }
}
