using FixLife.Admin.Identity.Enums;

namespace FixLife.Admin.Identity.Models
{
    public class AdminUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime PasswordValidDate { get; set; }

        public List<RoleEnum> Roles { get; set; }
    }
}
