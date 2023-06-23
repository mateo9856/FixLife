using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
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
        public PlanService(ApplicationContext context) : base(context) { }

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
