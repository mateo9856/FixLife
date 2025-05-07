using FixLife.Admin.Db.Entities.Base;
using FixLife.Admin.Db.Entities.Plans;

namespace FixLife.Admin.Db.Entities
{
    public class ClientPlan : EntityBase
    {
        public WeeklyWork WeeklyWork { get; set; }

        public LearnTime LearnTime { get; set; }

        public FreeTime FreeTime { get; set; }
    }
}
