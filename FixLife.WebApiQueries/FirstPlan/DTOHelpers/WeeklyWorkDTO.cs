using FixLife.WebApiDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan
{
    public class WeeklyWorkDTO
    {
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public List<DayOfWeeks> DayOfWeeks { get; set; }
    }
}
