using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.Enums
{
    public enum DayOfWeeks
    {
        Monday = 2 >> 1,
        Tuesday = 2 >> 2,
        Wednesday = 2 >> 3,
        Thursday = 2 >> 4,
        Friday = 2 >> 5,
        Saturday = 2 >> 6,
        Sunday = 2 >> 7
    }
}
