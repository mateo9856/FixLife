using FixLife.WebApiDomain.Common;
using FixLife.WebApiDomain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixLife.WebApiDomain.User
{
    public class ClientUser : UserEntity
    {
        [NotMapped]
        public ICollection<DependAccountsEnum> DependAccounts { get; set; }
    }
}
