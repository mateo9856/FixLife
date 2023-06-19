using FixLife.WebApiDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.Common
{
    public class DayOfWeek : BaseBusinessEntity
    {
        public DayOfWeeks Day { get; set; }
    }
}
