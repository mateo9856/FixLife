using FixLife.WebApiDomain.Common;
using FixLife.WebApiDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.Plan
{
    public class WeeklyWork : BaseBusinessEntity
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public DayOfWeeks DayOfWeeks { get; set; }
    }
}
