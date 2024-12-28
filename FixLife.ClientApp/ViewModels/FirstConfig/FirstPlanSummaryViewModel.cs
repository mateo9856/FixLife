using FixLife.ClientApp.Common;
using FixLife.ClientApp.Infrastructure.FirstPlan;
using FixLife.ClientApp.Models;
using FixLife.ClientApp.Models.FirstPlan;
using FixLife.ClientApp.Sessions;
using FixLife.ClientApp.Views.Popups;
using System.Text;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FirstPlanSummaryViewModel : BaseViewModel
    {
        private readonly WebApiClient<PlanCreateResponse> _webApiClient;
        public AppPlan SummaryPlan { get; set; }
        public bool IsEdit { get;set; }
        public List<string> FreeTimeSummaryTextView =>
            SummaryPlan.FreeTime.Select(d => string.Format("{0}:, start: {1}, end: {2}", d.Text, d.TimeStart.ToString(), d.TimeEnd.ToString())).ToList();
        public string WeeklyWorkTextView =>
            string.Format("Days: {0} Start: {1} End: {2}", string.Join(", ", SummaryPlan.WeeklyWork.DayOfWeeks),
                SummaryPlan.WeeklyWork.TimeStart, SummaryPlan.WeeklyWork.TimeEnd);
        public string LearnTimeSummaryTextView =>
            string.Format("Days: {0} Start: {1} Interval: {2}", string.Join(", ", SummaryPlan.LearnTime.DayOfWeeks),
                SummaryPlan.LearnTime.StartTime, SummaryPlan.LearnTime.TimeInterval);
        public string LearnTimeTextView { get; }
        public ICommand CreateCommand { get; private set; }
        public FirstPlanSummaryViewModel(WebApiClient<PlanCreateResponse> webApiClient)
        {
            SummaryPlan = FirstPlanSession.Instance().SummaryPlan;
            IsEdit = FirstPlanSession.Instance().IsEdit;
            CreateCommand = new Command(async () => await Create());
            _webApiClient = webApiClient;
        }

        private async Task ConsumeDataToKafka()
        {
            try
            {
                var builder = new StringBuilder();
                builder.Append(SummaryPlan.WeeklyWork.DayOfWeeks.Count() > 0 ? $"Weekly work: {WeeklyWorkTextView}/" : "-/");
                builder.Append(SummaryPlan.FreeTime.Count() > 0 ? $"Free time: {string.Join("|", FreeTimeSummaryTextView)}/" : "-/");
                builder.Append((SummaryPlan.LearnTime.DayOfWeeks.Count() > 0) ? $"Learn time: {LearnTimeSummaryTextView}/" : "-/");

                //Set to producerBuilder using DI:

                //var producer = new CreatePlanKafkaProducer();
                //await producer.CreateMessage(UserSession.Email, builder.ToString());
            
            } catch(Exception)
            {
                return;
            }
        }

        private async Task Create()
        {
            var requestBuilder = new
            {
                WeeklyWork = new
                {
                    TimeStart = SummaryPlan.WeeklyWork.TimeStart.ToString(),
                    TimeEnd = SummaryPlan.WeeklyWork.TimeEnd.ToString(),
                    DayOfWeeks = SummaryPlan.WeeklyWork.DayOfWeeks.ToList()
                },
                FreeTime = SummaryPlan.FreeTime.Where(d => !string.IsNullOrEmpty(d.Text))?.Select(c => new
                {
                    TimeStart = c.TimeStart.ToString(),
                    TimeEnd = c.TimeEnd.ToString(),
                    Text = c.Text
                }).ToList(),
                LearnTime = new
                {
                    TimeInterval = SummaryPlan.LearnTime.TimeInterval.ToString(),
                    StartTime = SummaryPlan.LearnTime.StartTime.ToString(),
                    DayOfWeeks = SummaryPlan.LearnTime.DayOfWeeks.ToList()

                }
            };

            try
            {
                PlanCreateResponse response = null;
                if (IsEdit)
                    response = await _webApiClient.PostPutAsync(requestBuilder, "FirstPlan/EditPlan", false, UserSession.Token);
                else
                    response = await _webApiClient.PostPutAsync(requestBuilder, "FirstPlan/createFirstPlan", true, UserSession.Token);

                if (response.Status == 201)
                {
                    await ConsumeDataToKafka();
                    await RedirectToPageAsync("//dash/DashboardPage");
                }
                else
                {
                    var popup = new ErrorPopup(string.Format("Error {0}", response.Status), response.Message);
                    await ShowPopup(popup);
                }
            } catch (Exception ex)
            {
                var popup = new ErrorPopup("Unexpected Error", ex.Message);
                await ShowPopup(popup);
            }
        }

    }
}
