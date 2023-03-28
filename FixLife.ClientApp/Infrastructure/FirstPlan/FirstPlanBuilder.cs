using FixLife.ClientApp.Models.AppPlan;
using FixLife.ClientApp.ViewModels.FirstConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Infrastructure.FirstPlan
{
    public class FirstPlanBuilder : IFirstPlanBuilder
    {
        public AppPlan Plan { get; set; }

        public FirstPlanBuilder()
        {
            Plan = new AppPlan();
        }

        public void ClearPlan()
        {
            Plan = new AppPlan();
        }

        public void SetFreeTime(FreeTimeViewModel model)
        {
            throw new NotImplementedException();
        }

        public void SetLearnTime(LearnTimeViewModel model)
        {
            throw new NotImplementedException();
        }

        public void SetWeeklyWork(WeeklyWorkViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
