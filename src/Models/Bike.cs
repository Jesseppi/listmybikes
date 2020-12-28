using Microsoft.EntityFrameworkCore;

namespace ListMyBikes.Models
{
    public interface IBike
    {
        long Id { get; set; }
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