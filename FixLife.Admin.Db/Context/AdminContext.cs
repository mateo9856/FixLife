using FixLife.Admin.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace FixLife.Admin.Db.Context
{
    public partial class AdminContext : DbContext
    {

        public AdminContext(DbContextOptions<AdminContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>()
                .IndexerProperty(typeof(Guid), "Id");

            base.OnModelCreating(modelBuilder);
        }
    }
}
