using FixLife.WebApiDomain.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardQueryResponse
    {
        public ICollection<FreeTime> FreeTime { get; set; }
        public LearnTime LearnTime { get; set; }
        public WeeklyWork WeeklyWork { get; set; }
    }
}
