using FixLife.WebApiDomain.Plan;

namespace FixLife.WebApiInfra.Abstraction.Dashboard
{
    public interface IDashboardService
    {
        Task<(short, Plan)> GetDashboardData(string user);
        Task<object> HandleDetectPush(string user);
    }
}
