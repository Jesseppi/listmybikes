using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListMyBikes.DAL;
using ListMyBikes.Data;
using ListMyBikes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ListMyBikes
{
    public class BikeRepository : IBikeRepository, IDisposable
    {
        private BikeContext _context;

        public BikeRepository(BikeContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Bike>> GetBikesAsync()
        {
            return await _context.Bikes.ToListAsync();
        }

        public async Task<Bike> GetBikeByIdAsync(long id)
        {
            return await _context.Bikes.FindAsync(id);
        }

        public async Task InsertBikeAsync(Bike bike)
        {
            await _context.Bikes.AddAsync(bike);
        }

        public async Task UpdateBikeAsync(Bike bike)
        {
            if (BikeExists(bike))
            {
                _context.Entry(bike).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
            }
        }


        public bool BikeExists(Bike bike) => BikeExists(bike.Id);
        public bool BikeExists(long bikeId) => _context.Bikes.Any(x => x.Id == bikeId);

        public async Task DeleteBikeAsync(long bikeId)
        {
            var bike = await _context.Bikes.FindAsync(bikeId);
            _context.Bikes.Remove(bike);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}