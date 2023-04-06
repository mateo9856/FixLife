using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models.AppPlan
{
    public class LearnTime
    {
        public bool IsYearly { get; set; }
        public bool IsWeekly { get; set; }
        public TimeSpan TimeInterval { get; set; }
        public DateTime StartTime { get; set; }

        public string TextView => ViewText();

        public string ViewText()
        {
            var strBuild = new StringBuilder();
            strBuild.Append(IsYearly ? "Yearly plan: " : IsWeekly ? "Weekly plan: " : "");
            strBuild.Append($"start: {StartTime.ToString("dd-MM-yyyy")} ");
            strBuild.Append($"daily time: {TimeInterval.ToString()}");
            return strBuild.ToString();
        }

    }
}
