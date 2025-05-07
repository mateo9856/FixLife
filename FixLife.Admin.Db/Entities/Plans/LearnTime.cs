using FixLife.Admin.Db.Entities.Base;

namespace FixLife.Admin.Db.Entities.Plans
{
    public class LearnTime : EntityBase
    {
        public TimeSpan TimeInterval { get; set; }
        public TimeSpan StartTime { get; set; }
        public ICollection<DayOfWeeks> DayOfWeeks { get; set; }
    }
}
