using ListMyBikes.DAL;
using Microsoft.EntityFrameworkCore;

namespace ListMyBikes.Models
{
    public interface IBike : IEntity
    {
        string Name { get; set; }
        long FrameId { get; set; }
    }

    public class BikeDTO : IBike
    {

        public long Id { get; set; }
        public string Name { get; set; }

        public long FrameId { get; set; }
    }

    public class Bike : IBike
    {

        public long Id { get; set; }
        public string Name { get; set; }

        public long FrameId { get; set; }
    }
}