using Farmify_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(u =>
            {
                u.HasMany(a => a.Addresses)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.Userid)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "Administrator",
                    Deleted = false
                },
                new Role
                {
                    Id = 2,
                    RoleName = "Customer",
                    Deleted = false
                },
                new Role
                {
                    Id = 3,
                    RoleName = "Farm Owner",
                    Deleted = false
                },
                new Role
                {
                    Id = 4,
                    RoleName = "Farm Manager",
                    Deleted = false
                },
                new Role
                {
                    Id = 5,
                    RoleName = "Farm Laborer",
                    Deleted = false
                });
        }
    }
}
