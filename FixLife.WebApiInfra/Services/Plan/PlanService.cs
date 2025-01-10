using FixLife.AI.Client.Abstraction;
using FixLife.WebApiDomain.Exceptions;
using FixLife.WebApiDomain.Models;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
using FixLife.WebApiInfra.Abstraction.Identity;
using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Common.Constants;
using FixLife.WebApiInfra.Contexts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace FixLife.WebApiInfra.Services
{
    public class PlanService : BaseService<Plan>, IPlanService
    {
        private readonly IMongoContextFactory<ApplicationContext> _context;
        private readonly IClientIdentityService _clientIdentityService;
        private readonly IPlanRecomendationService _recommendationService;

        public PlanService(IMongoContextFactory<ApplicationContext> dbFactory, IClientIdentityService clientIdentityService, IPlanRecomendationService recomendationService) : base(dbFactory)
        {
            _context = dbFactory;
            _clientIdentityService = clientIdentityService;
            _recommendationService = recomendationService;
        }

        public async Task AssignPlanToUserAsync(string userId, Plan plan)
        {
            var user = await _clientIdentityService.GetClientUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found! Cannot add plan!");
            }

            plan.UserId = user.Id;
        }

        public async Task<(short, string)> CreatePlanAsync(PlanModel planModel, bool isFirst, string userId)
        {
            try
            {
                using var ctx = await _context.CreateDbContextAsync();

                if (string.IsNullOrEmpty(userId))
                {
                    return (HttpCodes.NotFound, "User is not recognized!");
                }

                var plan = new Plan
                {
                    UserId = ObjectId.Parse(userId),
                    WeeklyWorkId = planModel.WeeklyWork.Id,
                    FreeTimeId = planModel.FreeTime.Select(d => d.Id).ToList() ?? new List<ObjectId>(),
                    LearnTimeId = planModel.LearnTime.Id
                };

                await ctx.WeeklyWorks.AddAsync(planModel.WeeklyWork);
                await ctx.LearnTimes.AddAsync(planModel.LearnTime);
                foreach (var freeTime in planModel.FreeTime)
                {
                    await ctx.FreeTimes.AddAsync(freeTime);
                }
                
                await ctx.Plans.AddAsync(plan);
                await AssignPlanToUserAsync(userId, plan);
                
                var createStatus = await ctx.SaveChangesAsync();
                if (createStatus > 0)
                {
                    return (HttpCodes.Created, "Created succesfully");
                }
                
                return (HttpCodes.NotFound, "Something wrong in database");
            }
            catch (Exception ex)
            {
                return (HttpCodes.InternalServerError, ex.Message);
            }

        }

        public async Task<(short, string)> EditPlanAsync(PlanModel planModel, PlanModel oldPlanModel, string userId)
        {
            try
            {
                using var ctx = await _context.CreateDbContextAsync();
                if (oldPlanModel == null)
                    return (HttpCodes.NotFound, "User plan not found!");

                UnassignOldPlan(oldPlanModel, ctx);

                await ctx.WeeklyWorks.AddAsync(planModel.WeeklyWork);
                await ctx.LearnTimes.AddAsync(planModel.LearnTime);
                foreach (var freeTime in planModel.FreeTime)
                {
                    await ctx.FreeTimes.AddAsync(freeTime);
                }
                ctx.Attach(planModel.WeeklyWork);
                ctx.Entry(planModel.WeeklyWork).State = EntityState.Modified;
                ctx.Attach(planModel.LearnTime);
                ctx.Entry(planModel.LearnTime).State = EntityState.Modified;
                foreach (var freeTime in planModel.FreeTime)
                {
                    ctx.Attach(freeTime);
                    ctx.Entry(freeTime).State = EntityState.Modified;
                }

                var createStatus = await ctx.SaveChangesAsync();

                if (createStatus > 0)
                {
                    return (HttpCodes.Created, "Modified succesfully");
                }
                return (HttpCodes.NotFound, "Something wrong in database");


            }
            catch (Exception ex)
            {
                return (HttpCodes.InternalServerError, ex.Message);
            }
        }

        public async Task<Plan?> GetPlanAsync(string userId)
        {
            using var ctx = await _context.CreateDbContextAsync();
            return await ctx.Plans
                .FirstOrDefaultAsync(d => d.UserId == ObjectId.Parse(userId));
        }

        public async Task<(short, string)> GetPlanIdAsync(string userId)
        {
            using var ctx = await _context.CreateDbContextAsync();
            var returnedPlan = await ctx.Plans.FirstOrDefaultAsync(d => d.UserId.ToString() == userId);
            return returnedPlan != null ? (HttpCodes.Ok, returnedPlan.UserId.ToString()) : (HttpCodes.NotFound, "");
        }

        public async Task<PlanModel?> GetPlanWithModel(string userId)
        {
            using var ctx = await _context.CreateDbContextAsync();
            var actualPlan = await GetPlanAsync(userId)
                ?? throw new ArgumentNullException(userId);

            var weeklyWork = await ctx.WeeklyWorks.FirstOrDefaultAsync(d => d.Id == actualPlan.WeeklyWorkId);
            var learnTime = await ctx.LearnTimes.FirstOrDefaultAsync(e => e.Id == actualPlan.LearnTimeId);
            var freeTimes = await ctx.FreeTimes.Where(f => actualPlan.FreeTimeId.Any(g => g.Equals(f.Id))).ToListAsync();

            return new PlanModel
            {
                UserId = userId,
                WeeklyWork = weeklyWork,
                LearnTime = learnTime,
                FreeTime = freeTimes
            };
        }

        public async Task<(short, List<string>?)> GetFreeTimeRecommendation(int count = 0)
        {
            var recomendations = await _recommendationService.GetFreeTimes(count);

            return recomendations.Count > 0 ? (HttpCodes.Ok, recomendations)
                : (HttpCodes.NotFound, null);
        }

        private void UnassignOldPlan(PlanModel oldPlan, ApplicationContext context)
        {
            oldPlan.WeeklyWork.DeletedDate = DateTime.Now;
            context.Entry(oldPlan.WeeklyWork).State = EntityState.Modified;
            oldPlan.LearnTime.DeletedDate = DateTime.Now;
            context.Entry(oldPlan.LearnTime).State = EntityState.Modified;
            oldPlan.FreeTime.ForEach((el) =>
            {
                el.DeletedDate = DateTime.Now;
                context.Entry(el).State = EntityState.Deleted;
            });

        }
    
    }
}
