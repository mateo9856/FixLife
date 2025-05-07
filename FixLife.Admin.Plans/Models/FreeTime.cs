namespace FixLife.Admin.Plans.Models
{
    public record FreeTime
    {
        public Guid Id { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string Text { get; set; }
    }
}
