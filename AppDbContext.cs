
using Farmify_Api.Models.Address;
using Farmify_Api.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Farmify_Api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Island> Islands { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<CityMunicipality> CityMunicipalities { get; set; }
        public DbSet<Barangay> Barangays { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Region>(d =>
            {
                d.HasOne(r => r.Island)
                .WithMany(r => r.Region)
                .HasForeignKey(r => r.Islandid);
            });

            modelBuilder.Entity<Province>(d =>
            {
                d.HasOne(p => p.Region)
                .WithMany(p => p.Province)
                .HasForeignKey(p => p.Regionid);
            });

            modelBuilder.Entity<CityMunicipality>(d =>
            {
                d.HasOne(c => c.Province)
                .WithMany(c => c.CityMunicipality)
                .HasForeignKey(c => c.Provinceid);
            });

            modelBuilder.Entity<Barangay>(d =>
            {
                d.HasOne(b => b.CityMunicipality)
                .WithMany(b => b.Barangay)
                .HasForeignKey(b => b.CityMunicipalityid);
            });
        }
    }
}
