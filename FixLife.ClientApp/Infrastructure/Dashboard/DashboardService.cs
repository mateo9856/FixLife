using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Infrastructure.Dashboard
{
    public class DashboardService : IDashboardService
    {
        public Task<BasicPlan> GetAppPlanData()
        {
            throw new NotImplementedException();
        }

        public Task SaveBasicDataToFile()
        {
            throw new NotImplementedException();
        }
    }
}
