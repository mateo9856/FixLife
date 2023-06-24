using FixLife.ClientApp.Common;
using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Models;
using FixLife.ClientApp.Models.Dashboard;
using FixLife.ClientApp.Models.Main;
using FixLife.ClientApp.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Infrastructure.Dashboard
{
    public class DashboardService : IDashboardService
    {
        public async Task<AppPlan> GetAppPlanData()
        {
            AppPlan plan;
            using (var client = new WebApiClient<DashboardData>())
            {
                var res = await client.CallServiceGetAsync("getdashboarddata", token: UserSession.Token);
                plan = res.Plan;
            }

            return plan;

        }

        public Task SaveBasicDataToFile()
        {
            throw new NotImplementedException();
        }
    }
}
