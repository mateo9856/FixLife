using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Entities;
using FixLife.Admin.Db.Implementations;
using FixLife.Admin.Plans.Abstractions;
using FixLife.Admin.Plans.Exceptions;
using FixLife.Admin.Plans.Models;
using Microsoft.EntityFrameworkCore;

namespace FixLife.Admin.Plans.Implementations
{
    public class PlanService : EntityOperationsBase<ClientPlan>, IPlanService
    {

        public PlanService(AdminContext adminContext) : base(adminContext)
        {
        }

        public async Task<(short, string)> AssignPlan(Guid userId, Plan plan)
        {
            var user = await GetUserById(userId);

            var clientPlan = new ClientPlan
            {
                Id = Guid.NewGuid(),
                FreeTime = plan.FreeTime,// find automapper alternative
                LearnTime = plan.LearnTime,
                WeeklyWork = plan.WeeklyWork,
                CreatedAt = DateTime.UtcNow,
            };

            throw new NotImplementedException();
        }

        public async Task<(short, string)> ConvertPlansToCsv(List<Guid> planIds)
        {
            throw new NotImplementedException();
        }

        public async Task<(short, string)> DeletePlan(Guid userId, Guid planId)
        {
            var user = GetUserById(userId);

            var clientPlan = await _dbTable.FirstOrDefaultAsync(d => d.Id == planId)
                ?? throw new PlanNotFoundException();

            Remove(clientPlan);

            throw new NotImplementedException();
        }

        public async Task<(short, string)> ModifyClientPlan(Guid userId, Plan plan)
        {
            var user = GetUserById(userId);

            var clientPlan = await _dbTable.FirstOrDefaultAsync(d => d.Id == plan.Id)
                ?? throw new PlanNotFoundException();

            clientPlan.FreeTime = plan.FreeTime;
            clientPlan.LearnTime = plan.LearnTime;
            clientPlan.WeeklyWork = plan.WeeklyWork;

            Update(clientPlan);

            throw new NotImplementedException();
        }

        private async Task<ClientUser> GetUserById(Guid userId)
        {
            return await _dbContext.ClientUsers.FirstOrDefaultAsync(d => d.Id == userId)
                ?? throw new UserNotFoundException();
        }
    }
}
