using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan.DTOHelpers
{
    public class PlanDTO
    {
        public WeeklyWorkDTO WeeklyWork { get; set; }
        public List<FreeTimeDTO> FreeTime { get; set; }
        public LearnTimeDTO LearnTime { get; set; }
    }
}
