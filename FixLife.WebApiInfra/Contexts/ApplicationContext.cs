using FixLife.WebApiDomain.Plan;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace FixLife.WebApiInfra.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
        }

        public DbSet<FreeTime> FreeTimes { get; set; }
        public DbSet<LearnTime> LearnTimes { get; set; }
        public DbSet<WeeklyWork> WeeklyWorks { get; set; }
        public DbSet<Plan> Plans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FreeTime>();
            modelBuilder.Entity<LearnTime>();
            modelBuilder.Entity<WeeklyWork>();
            modelBuilder.Entity<Plan>();
                
        }

    }
}
