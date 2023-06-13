using FixLife.WebApiDomain.Common;
using FixLife.WebApiDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.User
{
    public class ClientUser : UserEntity
    {
        [NotMapped]
        public ICollection<DependAccountsEnum> DependAccounts { get; set; }
    }
}
