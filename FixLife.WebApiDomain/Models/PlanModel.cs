using FixLife.WebApiDomain.Plan;

namespace FixLife.WebApiDomain.Models
{
    public class PlanModel
    {
        public string UserId { get; set; }
        public WeeklyWork WeeklyWork { get; set; }
        public List<FreeTime> FreeTime { get; set; }
        public LearnTime LearnTime { get; set; }
    }
}
