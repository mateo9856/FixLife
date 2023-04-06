using FixLife.ClientApp.Models.FirstPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models.AppPlan
{
    public class WeeklyWork
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public List<DayOfWeekListItem> DayOfWeeks { get; set; }

        public string TextView => ViewText();

        public string ViewText()
        {
            string days = string.Join(", ", DayOfWeeks.Select(d => d.Day.ToString()));
            var strBuild = new StringBuilder();
            strBuild.Append($"Works day: {days} ");
            strBuild.Append($"start: {TimeStart.ToString()}");
            strBuild.Append($"end: {TimeEnd.ToString()}");
            return strBuild.ToString();
        }
    }
}
