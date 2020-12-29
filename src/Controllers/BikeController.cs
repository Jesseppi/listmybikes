using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ListMyBikes.Data;
using ListMyBikes.Models;
using ListMyBikes.DAL;

namespace ListMyBikes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private IBikeRepository bikeRespository;

        public BikeController(BikeRepository repo)
        {
            this.bikeRespository = repo;
        }

        // GET: api/Bike
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bike>>> GetBikes()
        {
            var bikesList = await bikeRespository.GetBikesAsync();
            if (bikesList.Count() == 0) return NotFound();
            return Ok(bikesList);
        }

        // GET: api/Bike/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> GetBike(long id)
        {
            var bike = await bikeRespository.GetBikeByIdAsync(id);

            if (bike == null)
            {
                return NotFound();
            }

            return Ok(bike);
        }

        // PUT: api/Bike/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBike(long id, Bike bike)
        {
            if (id != bike.Id)
            {
                return BadRequest();
            }

            if (bikeRespository.BikeExists(id))
            {
                await bikeRespository.UpdateBikeAsync(bike);
                await bikeRespository.SaveAsync();
                return NoContent();
            }
            else
            {
                await bikeRespository.InsertBikeAsync(bike);
                await bikeRespository.SaveAsync();
                return CreatedAtAction(nameof(GetBike), new { id = bike.Id }, bike);
            }
        }

        // POST: api/Bike
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bike>> PostBike(Bike bike)
        {
            await bikeRespository.InsertBikeAsync(bike);
            await bikeRespository.SaveAsync();
            return CreatedAtAction(nameof(GetBike), new { id = bike.Id }, bike);
        }

        // DELETE: api/Bike/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBike(long id)
        {
            var bike = await bikeRespository.GetBikeByIdAsync(id);
            if (bike == null)
            {
                return NotFound();
            }

            await bikeRespository.DeleteBikeAsync(id);
            await bikeRespository.SaveAsync();
            return NoContent();
        }
    }
}
