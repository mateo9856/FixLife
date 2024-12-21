using FixLife.WebApiDomain.Plan;
using FixLife.WebApiQueries.FirstPlan;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardQueryResponse
    {
        public ICollection<FreeTimeDTO> FreeTime { get; set; }
        public LearnTimeDTO LearnTime { get; set; }
        public WeeklyWorkDTO WeeklyWork { get; set; }
    }
}
