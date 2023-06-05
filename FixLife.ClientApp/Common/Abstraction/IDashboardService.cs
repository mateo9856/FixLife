using FixLife.ClientApp.Models;
using FixLife.ClientApp.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Common.Abstraction
{
    public interface IDashboardService
    {
        Task SaveBasicDataToFile();
        Task<AppPlan> GetAppPlanData();
    }
}
