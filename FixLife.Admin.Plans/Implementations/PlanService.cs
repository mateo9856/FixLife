using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Entities;
using FixLife.Admin.Db.Implementations;
using FixLife.Admin.Db.Tools;
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
        private readonly CsvService<ClientPlan> _csvService;
        
        public PlanService(AdminContext adminContext) : base(adminContext)
        {
            _planMapper = new PlanMapper();
            _csvService = new();
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
            if (planIds is null || planIds.Count == 0)
                throw new ArgumentException("Plan IDs cannot be null or empty.", nameof(planIds));

            var plansList = new List<ClientPlan>();

            foreach (var planId in planIds)
            {
                var plan = await GetByIdAsync(planId);
                plansList.Add(plan);
            }

            if(plansList.Count <= 0)
                return ((short)400, "No plans were converted to CSV.");

            _csvService.SaveMultipleRecords($"DailyPlans_{DateTime.Now:yyyy:MM:dd}.csv", plansList);

            return ((short)200, $"{plansList.Count} plans converted to CSV successfully.");
        }

        public async Task<(short, string)> DeletePlan(Guid userId, Guid planId)
        {
            var user = GetUserById(userId);

            var clientPlan = await _dbTable.FirstOrDefaultAsync(d => d.Id == planId)
                ?? throw new PlanNotFoundException();

            BeginTransactionWithOperations(async () =>
            {
                clientPlan.WeeklyWork.DeletedAt = DateTime.UtcNow;
                clientPlan.LearnTime.DeletedAt = DateTime.UtcNow;
                clientPlan.FreeTime.DeletedAt = DateTime.UtcNow;

                var weeklyWork = _dbContext.Set<FixLife.Admin.Db.Entities.Plans.WeeklyWork>();
                weeklyWork.Attach(clientPlan.WeeklyWork);

                var freeTime = _dbContext.Set<FixLife.Admin.Db.Entities.Plans.FreeTime>();
                freeTime.Attach(clientPlan.FreeTime);

                var learnTime = _dbContext.Set<FixLife.Admin.Db.Entities.Plans.LearnTime>();
                learnTime.Attach(clientPlan.LearnTime);

                Remove(clientPlan);

                await SaveChangesAsync();
            });

            return (200, "Deleted successfully.");

        }

        public async Task<(short, string)> ModifyClientPlan(Guid userId, Plan plan)
        {
            var user = GetUserById(userId);

            var clientPlanExist = await _dbTable.AnyAsync(d => d.Id == plan.Id);

            if(clientPlanExist is not true)
                throw new PlanNotFoundException();

            var mapperPlan = _planMapper.Map(plan);

            Update(mapperPlan);

            await SaveChangesAsync();

            return (200, "Updated succesfully.");
        }

        private async Task<ClientUser> GetUserById(Guid userId)
        {
            return await _dbContext.ClientUsers.FirstOrDefaultAsync(d => d.Id == userId)
                ?? throw new UserNotFoundException();
        }
    }
}
