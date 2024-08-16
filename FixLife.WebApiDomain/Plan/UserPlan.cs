using FixLife.WebApiDomain.Common;
using FixLife.WebApiDomain.User;

namespace FixLife.WebApiDomain.Plan
{
    public class UserPlan : BaseBusinessEntity
    {
        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }
        public ClientUser User { get; set; }
        public Plan Plan { get; set; }
    }
}
