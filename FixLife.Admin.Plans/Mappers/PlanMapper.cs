using FixLife.Admin.Db.Entities;
using FixLife.Admin.Db.Tools.Abstraction;
using FixLife.Admin.Plans.Models;

namespace FixLife.Admin.Plans.Mappers
{
    public class PlanMapper : IModelMapper<Plan, ClientPlan>
    {
        private readonly IModelMapper<FixLife.Admin.Plans.Models.WeeklyWork, FixLife.Admin.Db.Entities.Plans.WeeklyWork> _weeklyWorkMapper;

        public PlanMapper()
        {
            _weeklyWorkMapper = new WeeklyWorkMapper();
        }

        public ClientPlan Map(Plan source)
        {
            return new ClientPlan
            {
                Id = source.Id,
                WeeklyWork = _weeklyWorkMapper.Map(source.WeeklyWork),
                FreeTime = null,
                LearnTime = null,
            };
        }
        public Plan MapBack(ClientPlan source)
        {
            return new Plan
            {
                Id = source.Id,
                WeeklyWork = _weeklyWorkMapper.MapBack(source.WeeklyWork),
                LearnTime = null,
                FreeTime = null,
            };
        }
    }
}
