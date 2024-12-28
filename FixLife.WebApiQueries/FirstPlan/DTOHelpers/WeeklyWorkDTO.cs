using FixLife.WebApiDomain.Enums;

namespace FixLife.WebApiQueries.FirstPlan
{
    public class WeeklyWorkDTO
    {
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public List<DayOfWeeks> DayOfWeeks { get; set; }
    }
}
