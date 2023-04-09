using FixLife.WebApiDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.Plan
{
    public class Plan : BaseBusinessEntity
    {
        public ICollection<FreeTime> FreeTime { get; set; }
        public LearnTime LearnTime { get; set; }
        public WeeklyWork WeeklyWork { get; set; }
    }
}
