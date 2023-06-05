using FixLife.ClientApp.Common;
using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Models;
using FixLife.ClientApp.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.Main
{
    public class DashboardViewModel : BaseViewModel
    {
        public ICommand EditPlanCommand { get; private set; }
        public AppPlan ActualPlan { get; set; }
        private readonly IDashboardService _dashboardService;
        public string WorkStatus { 
            get 
            {
                if (ActualPlan == null) return string.Empty;
                return InWorkState() ? "In work" : "After Work";
            } 
        }

        public bool TimesLeftVisible { get => InWorkState(); }
        public bool TimeToWorkVisible { get => !InWorkState(); }

        public DashboardViewModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            ActualPlan = new AppPlan();//TODO: Get Data from Api
            GetPlanData();
            EditPlanCommand = new Command(async e => await EditPlan());
        }

        private bool InWorkState()
        {
            if(ActualPlan == null || ActualPlan.WeeklyWork == null) return false;
            var nowDate = DateTime.Parse(DateTime.Now.ToString());
            var startDate = DateTime.Parse(ActualPlan.WeeklyWork.TimeStart.ToString());
            var endDate = DateTime.Parse(ActualPlan.WeeklyWork.TimeEnd.ToString());
            return nowDate > startDate && nowDate < endDate;        
        }

        private async void GetPlanData()
        {
            ActualPlan = await _dashboardService.GetAppPlanData();
        }

        private async Task EditPlan()
        {
            await Shell.Current.GoToAsync("FirstConfigPage");
        }

    }
}
