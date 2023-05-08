using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models
{
    public class LearnTime
    {
        public bool IsYearly { get; set; }
        public bool IsWeekly { get; set; }
        public TimeSpan TimeInterval { get; set; }
        public TimeSpan StartTime { get; set; }

    }
}
