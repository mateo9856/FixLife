using FixLife.WebApiDomain.Exceptions;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
using FixLife.WebApiInfra.Abstraction.Identity;
using FixLife.WebApiInfra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FixLife.WebApiInfra.Services
{
    public class PlanService : BaseService<Plan>, IPlanService
    {
        private readonly IClientIdentityService _clientIdentityService;
        public PlanService(ApplicationContext context, IClientIdentityService clientIdentityService) : base(context)
        {
            _clientIdentityService = clientIdentityService;
        }

        public async Task AssignPlanToUserAsync(string userId, Plan plan)
        {
            var user = await _clientIdentityService.GetClientUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found cannot add plan!");
            }

            plan.UserId = user.Id;
        }

        public async Task<(short, string)> CreatePlanAsync(Plan plan, bool isFirst, string userId)
        {
            try
            {
                Guid User;
                if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out User))
                {
                    return (400, "User is not recognized!");
                }
                plan.UserId = User;
                await _context.WeeklyWorks.AddAsync(plan.WeeklyWork);
                await _context.LearnTimes.AddAsync(plan.LearnTime);
                foreach (var freeTime in plan.FreeTime)
                {
                    await _context.FreeTimes.AddAsync(freeTime);
                }
                await _context.Plans.AddAsync(plan);
                await AssignPlanToUserAsync(userId, plan);
                var createStatus = await _context.SaveChangesAsync();
                if (createStatus > 0)
                {
                    return (201, "Created succesfully");
                }
                return (400, "Something wrong in database");
            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }

        }

        public async Task<(short, string)> EditPlanAsync(Plan plan, Plan oldPlan, string userId)
        {
            try
            {

                if (oldPlan == null)
                    return (404, "User plan not found!");

                UnassignOldPlan(oldPlan);

                plan.Id = oldPlan.Id;
                plan.UserId = oldPlan.UserId;
                plan.CreatedDate = oldPlan.CreatedDate;
                await _context.WeeklyWorks.AddAsync(plan.WeeklyWork);
                await _context.LearnTimes.AddAsync(plan.LearnTime);
                foreach (var freeTime in plan.FreeTime)
                {
                    await _context.FreeTimes.AddAsync(freeTime);
                }
                _context.Attach(plan);
                _context.Entry(plan).State = EntityState.Modified;

                var createStatus = await _context.SaveChangesAsync();

                if (createStatus > 0)
                {
                    return (201, "Modified succesfully");
                }
                return (400, "Something wrong in database");


            }
            catch (Exception ex)
            {
                return (500, ex.Message);
            }
        }

        public async Task<Plan?> GetPlanAsync(string userId)
        {
            return await _context.Plans.Include(d => d.WeeklyWork)
                .AsNoTracking()
                .Include(e => e.LearnTime)
                .Include(f => f.FreeTime)
                .FirstOrDefaultAsync(d => d.UserId == Guid.Parse(userId));
        }

        public async Task<(short, string)> GetPlanIdAsync(string userId)
        {
            var returnedPlan = await _context.Plans.FirstOrDefaultAsync(d => d.UserId.ToString() == userId);
            return returnedPlan != null ? ((short)200, returnedPlan.UserId.ToString()) : ((short)400, "");
        }

        private void UnassignOldPlan(Plan oldPlan)
        {
            oldPlan.WeeklyWork.DeletedDate = DateTime.Now;
            _context.Entry(oldPlan.WeeklyWork).State = EntityState.Modified;
            oldPlan.LearnTime.DeletedDate = DateTime.Now;
            _context.Entry(oldPlan.LearnTime).State = EntityState.Modified;

            foreach (var freeTime in oldPlan.FreeTime)
            {
                freeTime.DeletedDate = DateTime.Now;
                _context.Entry(freeTime).State = EntityState.Deleted;
            }
        }
    
    }
}
