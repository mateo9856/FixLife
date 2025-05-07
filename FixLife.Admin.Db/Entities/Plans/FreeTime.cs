using FixLife.Admin.Db.Entities.Base;

namespace FixLife.Admin.Db.Entities.Plans
{
    public class FreeTime : EntityBase
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string Text { get; set; }
    }
}
