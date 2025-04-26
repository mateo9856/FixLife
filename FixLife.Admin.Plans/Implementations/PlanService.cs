using FixLife.Admin.Plans.Abstractions;
using FixLife.Admin.Plans.Models;

namespace FixLife.Admin.Plans.Implementations
{
    internal class PlanService : IPlanService
    {
        public Task<(short, string)> AssignPlan(Guid userId, Plan plan)
        {
            throw new NotImplementedException();
        }

        public Task<(short, string)> ConvertPlansToCsv(List<Guid> planIds)
        {
            throw new NotImplementedException();
        }

        public Task<(short, string)> DeletePlan(Guid userId, Guid planId)
        {
            throw new NotImplementedException();
        }

        public Task<(short, string)> ModifyClientPlan(Guid userId, Plan plan)
        {
            throw new NotImplementedException();
        }
    }
}
