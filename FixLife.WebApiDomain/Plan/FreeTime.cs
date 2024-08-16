using FixLife.WebApiDomain.Common;

namespace FixLife.WebApiDomain.Plan
{
    public class FreeTime : BaseBusinessEntity
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string Text { get; set; }
    }
}
