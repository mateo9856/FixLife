using FixLife.ClientApp.Models.FirstPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models
{
    public class WeeklyWork
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public List<DayOfWeekListItem> DayOfWeeks { get; set; }

    }
}
