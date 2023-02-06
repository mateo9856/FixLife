using FixLife.WebApiDomain.Common;
using FixLife.WebApiDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.User
{
    public class ClientUser : UserEntity
    {
        public DependAccountsEnum[] DependAccounts { get; set; }
    }
}
