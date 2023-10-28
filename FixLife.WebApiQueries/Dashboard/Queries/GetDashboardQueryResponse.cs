using FixLife.WebApiDomain.Plan;
using FixLife.WebApiQueries.FirstPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardQueryResponse
    {
        public ICollection<FreeTimeDTO> FreeTime { get; set; }
        public LearnTimeDTO LearnTime { get; set; }
        public WeeklyWorkDTO WeeklyWork { get; set; }
    }
}
