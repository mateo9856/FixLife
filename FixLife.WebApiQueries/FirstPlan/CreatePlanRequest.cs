using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan
{
    public class CreatePlanRequest
    {
        public object WeeklyWork { get; set; }
        public object FreeTime { get; set; }
        public object LearnTime { get; set; }
    }
}
