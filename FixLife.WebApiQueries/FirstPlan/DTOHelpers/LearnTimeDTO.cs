using FixLife.WebApiDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan
{
    public class LearnTimeDTO
    {
        public bool IsYearly { get; set; }
        public bool IsWeekly { get; set; }
        public string TimeInterval { get; set; }
        public string StartTime { get; set; }
    }
}
