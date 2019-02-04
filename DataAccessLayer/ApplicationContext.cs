using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationContext : DbContext
    {
        public DbSet<RoleData> Roles { get; set; }

        public DbSet<UserData> Users { get; set; }

        public DbSet<TaskData> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=TODO;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserData>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<UserData>().HasIndex(u => u.Login).IsUnique();
            builder.Entity<RoleData>().HasIndex(u => u.Name).IsUnique();
        }
    }
}
