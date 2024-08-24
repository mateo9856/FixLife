using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using FixLife.WebApiInfra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FixLife.WebApiInfra.Services.Dashboard
{
    public class DashboardService : BaseService<Plan>, IDashboardService
    {
        private readonly IdentityContext _identityContext;
        public DashboardService(ApplicationContext context, IdentityContext identityContext) : base(context)
        {
            _identityContext = identityContext;
        }

        public async Task<(short, Plan)> GetDashboardData(string user)
        {
            var GetPlan = await _context.Plans
                .Include(d => d.WeeklyWork)
                .Include(e => e.LearnTime)
                .Include(f => f.FreeTime)
                .FirstOrDefaultAsync(d => d.UserId.ToString() == user);

            if (GetPlan != null)
            {
                var weeklyWorkDayOfWeeks = await _context.WeeklyWorks.SingleAsync(a => a.Id == GetPlan.WeeklyWork.Id);

                GetPlan.WeeklyWork = weeklyWorkDayOfWeeks;

                return (200, GetPlan);
            }

            return (404, null);
        }

        public async Task<object> HandleDetectPush(string user)
        {
            var GetPlan = await _context.Plans
            .Include(d => d.WeeklyWork)
            .Include(e => e.LearnTime)
            .Include(f => f.FreeTime)
            .FirstOrDefaultAsync(d => d.UserId.ToString() == user);
            if (GetPlan == null)
            {
                return new
                {
                    Status = 500,
                    TypeHeader = "Error",
                    Text = "Plan not found! Please create!"
                };
            }

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
                Status = 200,
                TextHeader = planToReturn?.Item1,
                Text = planToReturn.Item2
            };
        }
    }
}
