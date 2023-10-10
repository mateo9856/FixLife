using FixLife.ClientApp.Common;
using FixLife.ClientApp.Infrastructure.FirstPlan;
using FixLife.ClientApp.Sessions;
using FixLife.ClientApp.Views.FirstConfig.PlanViews;
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
                await CheckIfIsAddOrEdit(cmd.ToString());
                await Shell.Current.GoToAsync("//plan/CreatorPage");
            });
        }

        public void SetModel(object vm)
        {
            if (vm is WeeklyWorkViewModel)
                WeeklyWorkViewModel = (WeeklyWorkViewModel)vm;
            else if (vm is FreeTimeViewModel)
                FreeTimeViewModel = (FreeTimeViewModel)vm;
            else if (vm is LearnTimeViewModel)
                LearnTimeViewModel = (LearnTimeViewModel)vm;
        }

        public void SummaryPlan()
        {
            _firstPlanBuilder.SetWeeklyWork(WeeklyWorkViewModel);
            _firstPlanBuilder.SetFreeTime(FreeTimeViewModel);
            _firstPlanBuilder.SetLearnTime(LearnTimeViewModel);
            _firstPlanBuilder.ApplyPlan();
        }

        private async Task CheckIfIsAddOrEdit(string type)
        {
            var isEdit = _firstPlanBuilder.AssignTypeEdit(type.ToString());

            if (isEdit)
                await GetUserId();
        }

        private async Task GetUserId()
        {
            (short, string) response = (0, null);
            try
            {
                using (var client = new WebApiClient<(short, string)>())
                {
                    response = await client.CallServiceGetAsync(address: "FirstPlan/UserPlanId", token: UserSession.Token);
                }
                if (response.Item1 != 0)
                {
                    _firstPlanBuilder.AssignPlanId(response.Item2);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("API Connection error!");
            }
        }

    }
}
