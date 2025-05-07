using FixLife.Admin.Db.Entities.Base;
using FixLife.Admin.Db.Enums;

namespace FixLife.Admin.Db.Entities.Plans
{
    public class WeeklyWork : EntityBase
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public ICollection<DayOfWeeks> DayOfWeeks { get; set; }
    }
}
