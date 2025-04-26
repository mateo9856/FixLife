using FixLife.Admin.Plans.Models;

namespace FixLife.Admin.Plans.Abstractions
{
    internal interface IPlanService
    {
        Task<(short, string)> ModifyClientPlan(Guid userId, Plan plan);
        Task<(short, string)> AssignPlan(Guid userId, Plan plan);
        Task<(short, string)> DeletePlan(Guid userId, Guid planId);
        Task<(short, string)> ConvertPlansToCsv(List<Guid> planIds);
    }
}
