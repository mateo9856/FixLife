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
        private readonly IClientIdentityService _clientIdentityService;
        public PlanService(IMongoContextFactory<ApplicationContext> dbFactory, IClientIdentityService clientIdentityService) : base(dbFactory)
        {
            _clientIdentityService = clientIdentityService;
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

                await _context.WeeklyWorks.AddAsync(planModel.WeeklyWork);
                await _context.LearnTimes.AddAsync(planModel.LearnTime);
                foreach (var freeTime in planModel.FreeTime)
                {
                    await _context.FreeTimes.AddAsync(freeTime);
                }
                
                await _context.Plans.AddAsync(plan);
                await AssignPlanToUserAsync(userId, plan);
                
                var createStatus = await _context.SaveChangesAsync();
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

                if (oldPlanModel == null)
                    return (HttpCodes.NotFound, "User plan not found!");

                UnassignOldPlan(oldPlanModel);

                await _context.WeeklyWorks.AddAsync(planModel.WeeklyWork);
                await _context.LearnTimes.AddAsync(planModel.LearnTime);
                foreach (var freeTime in planModel.FreeTime)
                {
                    await _context.FreeTimes.AddAsync(freeTime);
                }
                _context.Attach(planModel.WeeklyWork);
                _context.Entry(planModel.WeeklyWork).State = EntityState.Modified;
                _context.Attach(planModel.LearnTime);
                _context.Entry(planModel.LearnTime).State = EntityState.Modified;
                foreach (var freeTime in planModel.FreeTime)
                {
                    _context.Attach(freeTime);
                    _context.Entry(freeTime).State = EntityState.Modified;
                }

                var createStatus = await _context.SaveChangesAsync();

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
            return await _context.Plans
                .FirstOrDefaultAsync(d => d.UserId == ObjectId.Parse(userId));
        }

        public async Task<(short, string)> GetPlanIdAsync(string userId)
        {
            var returnedPlan = await _context.Plans.FirstOrDefaultAsync(d => d.UserId.ToString() == userId);
            return returnedPlan != null ? (HttpCodes.Ok, returnedPlan.UserId.ToString()) : (HttpCodes.NotFound, "");
        }

        public async Task<PlanModel?> GetPlanWithModel(string userId)
        {
            var actualPlan = await GetPlanAsync(userId)
                ?? throw new ArgumentNullException(userId);

            var weeklyWork = await _context.WeeklyWorks.FirstOrDefaultAsync(d => d.Id == actualPlan.WeeklyWorkId);
            var learnTime = await _context.LearnTimes.FirstOrDefaultAsync(e => e.Id == actualPlan.LearnTimeId);
            var freeTimes = await _context.FreeTimes.Where(f => actualPlan.FreeTimeId.Any(g => g.Equals(f.Id))).ToListAsync();

            return new PlanModel
            {
                UserId = userId,
                WeeklyWork = weeklyWork,
                LearnTime = learnTime,
                FreeTime = freeTimes
            };
        }

        private void UnassignOldPlan(PlanModel oldPlan)
        {
            oldPlan.WeeklyWork.DeletedDate = DateTime.Now;
            _context.Entry(oldPlan.WeeklyWork).State = EntityState.Modified;
            oldPlan.LearnTime.DeletedDate = DateTime.Now;
            _context.Entry(oldPlan.LearnTime).State = EntityState.Modified;
            oldPlan.FreeTime.ForEach((el) =>
            {
                el.DeletedDate = DateTime.Now;
                _context.Entry(el).State = EntityState.Deleted;
            });

        }
    
    }
}
