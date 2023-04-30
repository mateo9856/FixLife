using CommunityToolkit.Maui.Views;
using FixLife.ClientApp.Common;
using FixLife.ClientApp.Infrastructure.FirstPlan;
using FixLife.ClientApp.Models.AppPlan;
using FixLife.ClientApp.Models.FirstPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FirstPlanSummaryViewModel
    {
        public AppPlan SummaryPlan { get; set; }
        public List<string> FreeTimeSummaryTextView =>
            SummaryPlan.FreeTime.Select(d => string.Format("{0}:, start: {1}, end: {2}", d.Text, d.TimeStart.ToString(), d.TimeEnd.ToString())).ToList();
        public string WeeklyWorkTextView =>
            string.Format("Days: {0} Start: {1} End: {2}", string.Join(", ", SummaryPlan.WeeklyWork.DayOfWeeks.Where(d => d.Selected).Select(d => d.Day)),
                SummaryPlan.WeeklyWork.TimeStart, SummaryPlan.WeeklyWork.TimeEnd);
        public string LearnTimeSummaryTextView =>
            string.Format("{0} Start: {1} Interval: {2}", (SummaryPlan.LearnTime.IsWeekly ? "WEEKLY PLAN" : "YEARLY PLAN"),
                SummaryPlan.LearnTime.StartTime, SummaryPlan.LearnTime.TimeInterval);
        public string LearnTimeTextView { get; }
        public ICommand CreateCommand { get; private set; }
        public FirstPlanSummaryViewModel()
        {
            SummaryPlan = FirstPlanSession.Instance().SummaryPlan;
            CreateCommand = new Command(async () => await Create());
        }

        private async Task Create()
        {
            //Request builder - Json does not support TimeSpan
            var requestBuilder = new
            {
                WeeklyWork = new
                {
                    TimeStart = SummaryPlan.WeeklyWork.TimeStart.ToString(),
                    TimeEnd = SummaryPlan.WeeklyWork.TimeEnd.ToString(),
                    DayOfWeeks = SummaryPlan.WeeklyWork.DayOfWeeks
                },
                FreeTime = SummaryPlan.FreeTime.Select(c => new
                {
                    TimeStart = c.TimeStart.ToString(),
                    TimeEnd = c.TimeEnd.ToString(),
                    Text = c.Text
                }),
                LearnTime = new
                {
                    IsYearly = SummaryPlan.LearnTime.IsYearly,
                    IsWeekly = SummaryPlan.LearnTime.IsWeekly,
                    TimeInterval = SummaryPlan.LearnTime.TimeInterval.ToString(),
                    StartTime = SummaryPlan.LearnTime.StartTime.ToString(),
                }
            };
      
            PlanCreateResponse response = null;
            using(var client = new WebApiClient<PlanCreateResponse>())
            {
                response = await client.PostPutAsync(requestBuilder, "FirstPlan/createFirstPlan", true);
            }
            if(response.Status == 201)
            {
                await Shell.Current.GoToAsync("DashboardPage");
            }
            else
            {
                
            }
        }

    }
}
