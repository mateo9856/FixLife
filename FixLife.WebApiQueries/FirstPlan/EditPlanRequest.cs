using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan
{
    public class EditPlanRequest
    {
        public WeeklyWorkDTO WeeklyWork { get; set; }
        public List<FreeTimeDTO> FreeTime { get; set; }
        public LearnTimeDTO LearnTime { get; set; }
    }
}
