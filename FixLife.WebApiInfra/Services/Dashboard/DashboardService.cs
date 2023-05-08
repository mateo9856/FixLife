using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        public Task<IEnumerable<Plan>> GetDashboardData(string user)
        {
            throw new NotImplementedException();
        }
    }
}
