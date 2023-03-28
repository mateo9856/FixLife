using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models.AppPlan
{
    public class AppPlan
    {
        public WeeklyWork WeeklyWork { get; set; }
        public FreeTime FreeTime { get; set; }
        public LearnTime LearnTime { get; set; }
    }
}
