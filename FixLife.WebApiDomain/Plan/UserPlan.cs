using FixLife.WebApiDomain.Common;
using FixLife.WebApiDomain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
