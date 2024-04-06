using FixLife.ClientApp.Common;
using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Common.Extensions;
using FixLife.ClientApp.Models;
using FixLife.ClientApp.Models.Dashboard;
using FixLife.ClientApp.Resources.Helpers;
using FixLife.ClientApp.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public ICommand EditPlanCommand { get; private set; }
        public AppPlan ActualPlan { get => UserSession.UserPlans; }
        private readonly IDashboardService _dashboardService;
        public string WorkStatus { 
            get 
            {
                if (ActualPlan == null) return string.Empty;
                return InWorkState() ? "In work" : "After Work";
            } 
        }

        public List<FreeTime> OrderedFreeTimes
        {
            get 
            { 
                return ActualPlan.FreeTime.Count() > 0 ? ActualPlan.FreeTime.OrderBy(a => a.TimeStart).ToList() : new List<FreeTime>(); 
            }
        }

        public bool TimesLeftVisible { get => InWorkState(); }
        public bool TimeToWorkVisible { get => !InWorkState(); }

        public string TimeToEndWork { 
            get
            {
                var timeLeft = ActualPlan.WeeklyWork.TimeEnd.Subtract(DateTime.Now.ParseToTimeSpan());
                return $"Time to left: {timeLeft.ToString()}";
            } 
        }

        public string TimeToStartWork
        {
            get
            {
                var timeLeft = ActualPlan.WeeklyWork.TimeStart.Subtract(DateTime.Now.ParseToTimeSpan()).AbsTime();
                return $"Time to work: {timeLeft.ToString()}";
            }
        }

        public string TimeToLearn
        {
            get
            {
                var learnTime = ActualPlan.LearnTime.StartTime.Subtract(DateTime.Now.ParseToTimeSpan()).AbsTime();
                return $"Time to learn: {learnTime.ToString()}";
            }
        }

        public string ActualFreeTime
        {
            get
            {
                if(OrderedFreeTimes.Count <= 0) return "You have not any Free Times :(";
                var firstFreeTime = OrderedFreeTimes.FirstOrDefault(d => d.TimeStart >= DateTime.Now.ParseToTimeSpan());
                return firstFreeTime == null ? "You have not actual free times :(" : $"Now: {firstFreeTime.Text}";
            }
        }

        public string NextFreeTime
        {
            get 
            {
                if (OrderedFreeTimes.Count <= 1) return string.Empty;
                return $"Next: {OrderedFreeTimes[1]?.Text}";
            }
        }

        public DashboardViewModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            Task t = new Task(async () =>
            {
                UserSession.UserPlans = _dashboardService.GetAppPlanData();
            });
            t.Start();
            Task.WaitAll(t);

            EditPlanCommand = new Command(async () => await EditPlan());
        }

        private bool InWorkState()
        {
            if(ActualPlan == null || ActualPlan.WeeklyWork == null) return false;
            var nowDate = DateTime.Parse(DateTime.Now.ToString());
            var startDate = DateTime.Parse(ActualPlan.WeeklyWork.TimeStart.ToString());
            var endDate = DateTime.Parse(ActualPlan.WeeklyWork.TimeEnd.ToString());
            return nowDate > startDate && nowDate < endDate;        
        }

        private async Task EditPlan()
        {
           await Shell.Current.GoToAsync("EditConfigPage");
        }

    }
}
