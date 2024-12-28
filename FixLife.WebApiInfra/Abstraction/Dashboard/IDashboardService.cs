using FixLife.WebApiDomain.Models;

namespace FixLife.WebApiInfra.Abstraction.Dashboard
{
    public interface IDashboardService
    {
        Task<(short, PlanModel)> GetDashboardData(string user);
        Task<object> HandleDetectPush(string user);
    }
}
