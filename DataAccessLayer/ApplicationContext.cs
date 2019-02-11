using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            base.ChangeTracker.AutoDetectChangesEnabled = false;
            base.ChangeTracker.LazyLoadingEnabled = false;
            Database.EnsureCreated();
        }

        public DbSet<RoleData> Roles { get; set; }

        public DbSet<UserData> Users { get; set; }

        public DbSet<TaskData> Tasks { get; set; }

        public DbSet<SessionData> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserData>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<UserData>().HasIndex(u => u.Login).IsUnique();
            builder.Entity<RoleData>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<SessionData>().Property(x => x.Id).IsRequired().ValueGeneratedNever();
        }
    }
}
