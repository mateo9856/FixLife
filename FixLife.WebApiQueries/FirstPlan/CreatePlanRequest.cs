using FixLife.WebApiDomain.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan
{
    public class CreatePlanRequest
    {
        public WeeklyWork WeeklyWork { get; set; }
        public List<FreeTime> FreeTime { get; set; }
        public LearnTime LearnTime { get; set; }
    }
}
