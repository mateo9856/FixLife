using FixLife.WebApiDomain.Common;

namespace FixLife.WebApiDomain.Plan
{
    public class Plan : BaseBusinessEntity
    {
        public ICollection<FreeTime> FreeTime { get; set; }
        public LearnTime LearnTime { get; set; }
        public WeeklyWork WeeklyWork { get; set; }
        public Guid UserId { get; set; }
    }
}
