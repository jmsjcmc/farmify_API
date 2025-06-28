
using Farmify_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Role)
                .WithMany(u => u.User)
                .UsingEntity(j => j.ToTable("UserRoles"));

            modelBuilder.Entity<Farm>(d =>
            {
                d.HasOne(f => f.User)
                .WithOne(f => f.Farm)
                .HasForeignKey<Farm>(f => f.Userid);
            });

            modelBuilder.Entity<Product>(d =>
            {
                d.HasOne(p => p.Category)
                .WithMany(p => p.Product)
                .HasForeignKey(p => p.Categoryid);
            });
        }
    }
}
