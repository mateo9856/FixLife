using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Common.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan AbsTime(this TimeSpan time)
        {
            return time.TotalSeconds < 0 ? time.Add(new TimeSpan(1, 0, 0, 0)) : time;
        }
    }
}
