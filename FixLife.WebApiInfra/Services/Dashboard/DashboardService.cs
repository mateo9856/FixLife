using FixLife.WebApiDomain.Models;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Common.Constants;
using FixLife.WebApiInfra.Contexts;

namespace FixLife.WebApiInfra.Services.Dashboard
{
    public class DashboardService : BaseService<Plan>, IDashboardService
    {
        private readonly IPlanService _planService;

        public DashboardService(IMongoContextFactory<ApplicationContext> dbFactory, IPlanService planService) : base(dbFactory)
        {
            _planService = planService;
        }

        public async Task<(short, PlanModel)> GetDashboardData(string user)
        {
            var GetPlan = await GetByIdAsync(user);

            if (GetPlan == null)
            {
                return (HttpCodes.NotFound, null);   
            }

            var plans = await _planService.GetPlanWithModel(user);

            return (HttpCodes.Ok, plans);
        }

        public async Task<object> HandleDetectPush(string user)
        {
            var GetPlan = await _planService.GetPlanWithModel(user);
            if (GetPlan == null)
            {
                return new
                {
                    Status = 500,
                    TypeHeader = "Error",
                    Text = "Plan not found! Please create!"
                };
            }
            //TODO: Refactor and create DashboardDataDTO
            var dNow = DateTime.Now;
            var actualTimeSpan = new TimeSpan(0, dNow.Hour, dNow.Minute, dNow.Second);

            var timesList = new List<Tuple<string, TimeSpan>>();

            if(GetPlan.WeeklyWork != null)
                timesList.Add(Tuple.Create("Weekly work", actualTimeSpan.Subtract(GetPlan.WeeklyWork.TimeStart)));
            
            if(GetPlan.LearnTime != null)
                timesList.Add(Tuple.Create("Learn time", actualTimeSpan.Subtract(GetPlan.LearnTime.StartTime)));

            foreach (var item in GetPlan.FreeTime)
            {
                timesList.Add(Tuple.Create("Free time", actualTimeSpan.Subtract(item.TimeStart)));
            }
            var planToReturn = timesList.Where(a => a.Item2.Hours == 0)
                .OrderBy(d => d.Item2.Minutes)
                .ThenBy(e => e.Item2.Seconds)
                .FirstOrDefault();
            return new
            {
                Status = HttpCodes.Ok,
                TextHeader = planToReturn?.Item1,
                Text = planToReturn.Item2
            };
        }
    }
}
