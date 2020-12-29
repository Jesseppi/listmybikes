using ListMyBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace ListMyBikes.Data
{
    public class BikeContext : DbContext
    {
        public BikeContext(DbContextOptions<BikeContext> options) : base(options) { }

        public DbSet<Bike> Bikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Bike>().ToTable("Bike");
        }
    }
}