﻿using FixLife.WebApiDomain.User;
using Microsoft.EntityFrameworkCore;

namespace FixLife.WebApiInfra.Contexts
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) 
        {
        }

        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>().HasKey(x => x.Id);
            modelBuilder.Entity<ClientUser>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

    }
}
