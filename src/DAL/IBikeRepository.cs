using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListMyBikes.Models;

namespace ListMyBikes.DAL
{
    public interface IBikeRepository : IDisposable
    {
        Task<IEnumerable<Bike>> GetBikesAsync();
        Task<Bike> GetBikeByIdAsync(long id);
        Task InsertBikeAsync(Bike bike);
        Task DeleteBikeAsync(long bikeId);
        Task UpdateBikeAsync(Bike bike);
        Task SaveAsync();

        bool BikeExists(Bike bike);

        bool BikeExists(long bikeId);
    }
}