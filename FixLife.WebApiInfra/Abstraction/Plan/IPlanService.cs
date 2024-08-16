using FixLife.WebApiDomain.Plan;

namespace FixLife.WebApiInfra.Abstraction
{
    public interface IPlanService
    {
        Task<(short, string)> CreatePlanAsync(Plan plan, bool isFirst, string userId);
        Task AssignPlanToUserAsync(string userId, Plan plan);
        Task<(short, string)> GetPlanIdAsync(string userId);
        Task<(short, string)> EditPlanAsync(Plan plan, Plan oldPlan, string userId);
        Task<Plan?> GetPlanAsync(string userId);
    }
}
