using System.Linq;
using ListMyBikes.Models;

namespace ListMyBikes.Data
{
    public static class DbInitialiser
    {
        public static void Initialise(BikeContext context)
        {
            context.Database.EnsureCreated();

            if (context.Bikes.Any())
            {
                return; // Db has been seeded already
            }

            var bikes = new Bike[]
            {
                new Bike {
                    Name = "Stinner",
                    FrameId = 1
                }
            };

            foreach(var bike in bikes){
                context.Add(bike);
            }
            context.SaveChanges();
        }
    }
}