using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeRental.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BikeRental.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly BikeContext _context;
        private readonly GenderParser genderParser;

        public CustomerController(ILogger<CustomerController> logger, BikeContext context)
        {
            _logger = logger;
            _context = context;
            genderParser = new GenderParser();
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll(string filterValue = "")
        {
            //return _context.Customers.ToList();
            return await _context.Customers.Where(p => p.LastName.Contains(filterValue)).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (customer == null)
                return BadRequest();

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
                return BadRequest();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("{customerId}/rentals")]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentalsForCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
                return BadRequest();

            return customer.Rentals.ToList();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
