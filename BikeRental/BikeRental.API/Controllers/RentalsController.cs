using System;
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
    public class RentalsController : ControllerBase
    {
        private readonly BikeContext _context;
        private readonly RentalCostCalculator rentalCostCalculator;

        public RentalsController(BikeContext context)
        {
            _context = context;
            rentalCostCalculator = new RentalCostCalculator();
        }

        /*
         *  Get a list of unpaid, ended rentals with total price > 0. For each rental, the following data must be returned:
            Customer's ID, first and last name
            Rental's ID, start end, end date, and total price
        */
        [HttpGet]
        public async Task<System.Collections.Generic.IEnumerable<Rental>> GetRentals()
        {
            var rentals = await _context.Rentals.Where(r => r.Paid == false && r.RentalEnd != null).ToListAsync();
            var result = rentals.Where(r => ((DateTime) r.RentalEnd).Subtract(r.RentalBegin).TotalMinutes >= 15);
            return result;
        }

        // PUT: api/Rentals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}/end")]
        public async Task<IActionResult> EndRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            rental.RentalEnd = DateTime.Now;
            rental.TotalPrice = rentalCostCalculator.Calculate(rental);

            rental.Bike.CurrentlyRented = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPut("{id}/pay")]
        public async Task<ActionResult<Rental>> PayRental(int id)
        {
            var rental = _context.Rentals.Find(id);

            if (rental == null)
                return BadRequest("Rental is null");

            if (rental.TotalPrice <= 0)
                return BadRequest("Total Price is 0");

            rental.Paid = true;

            _context.Rentals.Update(rental);

            await _context.SaveChangesAsync();
            return Ok("Rental Successfully Paid");
        }

        // POST: api/Rentals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rental>> StartRental(Rental rental)
        {
            var customer = await _context.Customers.FindAsync(rental.CustomerId);
            var bike = await _context.Bikes.FindAsync(rental.BikeId);

            if (bike.CurrentlyRented)
                return BadRequest("Bike already rented");

            var customerCurrentRentals = customer.Rentals.Where(r => r.RentalEnd != null);

            if (customerCurrentRentals.Count() > 0)
                return BadRequest("Customer already has rented a bike");

            bike.CurrentlyRented = true;

            rental.RentalBegin = DateTime.Now;
            rental.TotalPrice = 0.0m;
            rental.Paid = false;
            rental.Customer = customer;
            rental.Bike = bike;

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return Ok(rental);
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalId == id);
        }
    }
}