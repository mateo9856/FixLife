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
        public ClientUser Users { get; set; }
        public Plan Plans { get; set; }
    }
}
