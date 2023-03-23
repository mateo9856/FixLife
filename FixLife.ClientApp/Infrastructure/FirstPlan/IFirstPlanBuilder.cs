using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Infrastructure.FirstPlan
{
    public interface IFirstPlanBuilder
    {
        void ClearPlan();
        void SetWeeklyWork();
        void SetFreeTime();
        void SetLearnTime();
    }
}
