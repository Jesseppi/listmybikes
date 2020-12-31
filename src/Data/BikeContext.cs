using ListMyBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace ListMyBikes.Data
{
    public class BikeContext : DbContext
    {
        public BikeContext(){}
        public BikeContext(DbContextOptions<BikeContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseNpgsql("user ID=jessec;Password=;Host=localhost;Port=5432;Database=ListMyBikes");

        public DbSet<Bike> Bikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Bike>().ToTable("Bike");
        }
    }
}