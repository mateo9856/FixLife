using FixLife.Admin.Db.Entities;
using FixLife.Admin.Db.Entities.Plans;
using Microsoft.EntityFrameworkCore;

namespace FixLife.Admin.Db.Context
{
    public partial class AdminContext : DbContext
    {

        public AdminContext(DbContextOptions<AdminContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<AdminUser> Users { get; set; }

        public DbSet<ClientPlan> ClientPlans { get; set; }

        public DbSet<ClientUser> ClientUsers { get; set; }

        public DbSet<WeeklyWork> WeeklyWorks { get; set; }

        public DbSet<FreeTime> FreeTimes { get; set; }

        public DbSet<LearnTime> LearnTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
