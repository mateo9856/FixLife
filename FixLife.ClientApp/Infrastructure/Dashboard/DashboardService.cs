using FixLife.ClientApp.Common;
using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Models;
using FixLife.ClientApp.Sessions;

namespace FixLife.ClientApp.Infrastructure.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly WebApiClient<AppPlan> _webApiClient;

        public DashboardService(WebApiClient<AppPlan> webApiClient)
        {
            _webApiClient = webApiClient;
        }

        public async Task<AppPlan> GetAppPlanData()
        {
            var result = await _webApiClient.CallServiceGetAsync("UserDashboard/getdashboarddata", token: UserSession.Token);

            return result;

        }

        public Task SaveBasicDataToFile()
        {
            throw new NotImplementedException();
        }
    }
}
