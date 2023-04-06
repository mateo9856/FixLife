using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models.AppPlan
{
    public class FreeTime
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string Text { get; set; }

        public string TextView => string.Format("{0}:, start: {1}, end: {2}", Text, TimeStart.ToString(), TimeEnd.ToString());
    }
}
