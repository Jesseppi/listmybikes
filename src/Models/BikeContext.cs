using Microsoft.EntityFrameworkCore;

namespace ListMyBikes.Models
{
    public class BikeContext : DbContext
    {
        public BikeContext(DbContextOptions<BikeContext> options) : base(options) { }

        public DbSet<Bike> Bikes { get; set; }
    }
}