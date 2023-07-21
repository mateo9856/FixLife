using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Resources.Helpers
{
    public static class DateToTimeSpan
    {
        public static TimeSpan ParseToTimeSpan(this DateTime date)
        {
            return new TimeSpan(0, date.Hour,date.Minute,date.Second);
        }
    }
}
