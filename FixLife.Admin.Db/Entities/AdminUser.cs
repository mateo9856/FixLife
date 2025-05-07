using FixLife.Admin.Db.Entities.Base;
using FixLife.Admin.Db.Enums;

namespace FixLife.Admin.Db.Entities
{
    public class AdminUser : EntityBase
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime PasswordValidDate { get; set; }

        public List<RoleEnum> Roles { get; set; }
    }
}
