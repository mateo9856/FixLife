using FixLife.Admin.Identity.Models;
using FixLife.Admin.Plans.Models;
using FixLife.Admin.Users.Models;
using Microsoft.EntityFrameworkCore;


namespace FixLife.Admin.Db.Context
{
    public partial class AdminContext : DbContext
    {
        public DbSet<Plan> Plans { get; set; }
        public DbSet<AdminUser> Users { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
    }
}
