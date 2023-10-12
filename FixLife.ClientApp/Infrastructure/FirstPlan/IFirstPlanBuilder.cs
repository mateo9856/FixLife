using FixLife.ClientApp.Models;
using FixLife.ClientApp.ViewModels.FirstConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Infrastructure.FirstPlan
{
    public interface IFirstPlanBuilder
    {
        AppPlan Plan { get; set; }
        void ClearPlan();
        void SetWeeklyWork(WeeklyWorkViewModel model);
        void SetFreeTime(FreeTimeViewModel model);
        void SetLearnTime(LearnTimeViewModel model);
        void ApplyPlan();
        bool AssignTypeEdit(string type);
    }
}
