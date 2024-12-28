using FixLife.WebApiDomain.Enums;

namespace FixLife.WebApiQueries.FirstPlan
{
    public class LearnTimeDTO
    {
        public string TimeInterval { get; set; }
        public string StartTime { get; set; }
        public List<DayOfWeeks> DayOfWeeks { get; set; }
    }
}
