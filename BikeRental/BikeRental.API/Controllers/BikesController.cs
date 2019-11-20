using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRental.Library;

namespace BikeRental.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private readonly BikeContext _context;

        public BikesController(BikeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bike>>> GetBikes(string filter = "none")
        {
            switch (filter)
            {
                case "price-first-hour-accending": return await _context.Bikes.Where(p => p.CurrentlyRented == false).OrderBy(p => p.RentalPriceFirstHour).ToListAsync();
                case "price-additional-hours-accending": return await _context.Bikes.Where(p => p.CurrentlyRented == false).OrderBy(p => p.RentalPriceAdditionalHour).ToListAsync();
                case "purchase-date-decending": return await _context.Bikes.Where(p => p.CurrentlyRented == false).OrderByDescending(p => p.PurchaseDate).ToListAsync();
                case "none":
                default:
                    return await _context.Bikes.Where(p => p.CurrentlyRented == false).ToListAsync();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBike(int id, Bike bike)
        {
            if (id != bike.BikeId)
            {
                return BadRequest();
            }

            _context.Entry(bike).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Bike>> PostBike(Bike bike)
        {
            bike.CurrentlyRented = false;
            _context.Bikes.Add(bike);
            await _context.SaveChangesAsync();

            return bike;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Bike>> DeleteBike(int id)
        {
            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null)
            {
                return NotFound();
            }

            _context.Bikes.Remove(bike);
            await _context.SaveChangesAsync();

            return bike;
        }

       
    }
}
