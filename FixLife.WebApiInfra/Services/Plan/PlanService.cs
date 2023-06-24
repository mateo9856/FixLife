using FixLife.WebApiDomain.Exceptions;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
using FixLife.WebApiInfra.Abstraction.Identity;
using FixLife.WebApiInfra.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra.Services
{
    public class PlanService : BaseService<Plan>, IPlanService
    {
        private readonly IClientIdentityService _clientIdentityService;
        public PlanService(ApplicationContext context, IClientIdentityService clientIdentityService) : base(context) { 
            _clientIdentityService = clientIdentityService;
        }

        public async Task AssignPlanToUserAsync(string userId, Plan plan)
        {
            var user = await _clientIdentityService.GetClientUser(userId);
            if (user == null) {
                throw new UserNotFoundException("User not found cannot add plan!");
            }
            
            var userPlan = new UserPlan
            {
                Users = user,
                Plans = plan
            };
            await _context.UserPlan.AddAsync(userPlan);
        }

        public async Task<(short, string)> CreatePlanAsync(Plan plan, bool isFirst, string userId)
        {
            try
            {
                Guid User;
                if(string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out User))
                {
                    return (400, "User is not recognized!");
                }
                plan.UserId = User;
                await _context.WeeklyWorks.AddAsync(plan.WeeklyWork);
                await _context.LearnTimes.AddAsync(plan.LearnTime);
                foreach(var freeTime in plan.FreeTime)
                {
                    await _context.FreeTimes.AddAsync(freeTime);
                }
                await _context.Plans.AddAsync(plan);
                await AssignPlanToUserAsync(userId, plan);
                var createStatus = await _context.SaveChangesAsync();
                if(createStatus > 0)
                {
                    return (201, "Created succesfully");
                }
                return (400, "Something wrong in database");
            } 
            catch(Exception ex)
            {
                return (500, ex.Message);
            }

        }
    }
}
