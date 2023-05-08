using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models
{
    public class AppPlan
    {
        public WeeklyWork WeeklyWork { get; set; }
        public List<FreeTime> FreeTime { get; set; }
        public LearnTime LearnTime { get; set; }
    }
}
