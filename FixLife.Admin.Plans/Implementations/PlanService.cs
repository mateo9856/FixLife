using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Entities;
using FixLife.Admin.Db.Implementations;
using FixLife.Admin.Db.Tools.Abstraction;
using FixLife.Admin.Plans.Abstractions;
using FixLife.Admin.Plans.Exceptions;
using FixLife.Admin.Plans.Mappers;
using FixLife.Admin.Plans.Models;
using Microsoft.EntityFrameworkCore;

namespace FixLife.Admin.Plans.Implementations
{
    public class PlanService : EntityOperationsBase<ClientPlan>, IPlanService
    {
        private readonly IModelMapper<Plan, ClientPlan> _planMapper;

        public PlanService(AdminContext adminContext) : base(adminContext)
        {
            _planMapper = new PlanMapper();
        }

        public async Task<(short, string)> AssignPlan(Guid userId, Plan plan)
        {
            var user = await GetUserById(userId);

            var clientPlan = _planMapper.Map(plan);
            clientPlan.Id = Guid.NewGuid();
            clientPlan.CreatedAt = DateTime.UtcNow;

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

            var clientPlanExist = await _dbTable.AnyAsync(d => d.Id == plan.Id);

            if(clientPlanExist is not true)
                throw new PlanNotFoundException();

            var mapperPlan = _planMapper.Map(plan);

            Update(mapperPlan);

            throw new NotImplementedException();
        }

        private async Task<ClientUser> GetUserById(Guid userId)
        {
            return await _dbContext.ClientUsers.FirstOrDefaultAsync(d => d.Id == userId)
                ?? throw new UserNotFoundException();
        }
    }
}
