using FixLife.WebApiDomain.Models;
using FixLife.WebApiDomain.Plan;

namespace FixLife.WebApiInfra.Abstraction
{
    public interface IPlanService
    {
        Task<(short, string)> CreatePlanAsync(PlanModel planModel, bool isFirst, string userId);
        Task<(short, string)> GetPlanIdAsync(string userId);
        Task<(short, List<string>?)> GetWeeklyWorkRecommendation();
        Task<(short, string)> EditPlanAsync(PlanModel planModel, PlanModel oldPlanModel, string userId);
        Task<Plan?> GetPlanAsync(string userId);
        Task<PlanModel?> GetPlanWithModel(string userId);
    }
}
