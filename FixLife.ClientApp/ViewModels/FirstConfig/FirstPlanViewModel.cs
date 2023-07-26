using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Infrastructure.FirstPlan;
using FixLife.ClientApp.Models.FirstPlan;
using FixLife.ClientApp.Views.FirstConfig.PlanViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FirstPlanViewModel : BaseViewModel
    {
        private IFirstPlanBuilder _firstPlanBuilder;
        public FreeTime FreeTime { get; set; }
        public WeeklyWork WeeklyWork { get; set; }
        public LearnTime LearnTime { get; set; }
        public WeeklyWorkViewModel WeeklyWorkViewModel { get; set; }
        public FreeTimeViewModel FreeTimeViewModel { get; set; }
        public LearnTimeViewModel LearnTimeViewModel { get; set; }
        public ICommand SetImageCommand { get; private set; }
        public ICommand CreateCommand { get; private set; }
        public FirstPlanViewModel()
        {
            _firstPlanBuilder = new FirstPlanBuilder();
            _firstPlanBuilder.ClearPlan();
            CreateCommand = new Command(async (cmd) =>
            {
                await Shell.Current.GoToAsync("//plan/CreatorPage");
            });
        }

        public void SetModel(object vm)
        {
            if(vm is WeeklyWorkViewModel)
                WeeklyWorkViewModel = (WeeklyWorkViewModel)vm;
            else if(vm is FreeTimeViewModel)
                FreeTimeViewModel = (FreeTimeViewModel)vm;
            else if(vm is LearnTimeViewModel)
                LearnTimeViewModel = (LearnTimeViewModel)vm;
        }

        public void SummaryPlan()
        {
            _firstPlanBuilder.SetWeeklyWork(WeeklyWorkViewModel);
            _firstPlanBuilder.SetFreeTime(FreeTimeViewModel);
            _firstPlanBuilder.SetLearnTime(LearnTimeViewModel);
            _firstPlanBuilder.ApplyPlan();
        }

    }
}
