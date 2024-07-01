using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using QRMenuAPI.Models;
using QRMenuAPI.Data;

namespace QRMenuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RestaurantController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Restaurant
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurant()
        {
          if (_context.Restaurant == null)
          {
              return NotFound();
          }
            return await _context.Restaurant.ToListAsync();
        }

        // GET: api/Restaurant/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
          if (_context.Restaurant == null)
          {
              return NotFound();
          }
            var restaurant = await _context.Restaurant.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        // PUT: api/Restaurant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "CompanyAdministrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/Restaurant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "CompanyAdministrator")]
        [HttpPost]
        public ActionResult PostRestaurant(Restaurant restaurant)
        {
          if (_context.Restaurant == null)
          {
              return Problem("Entity set 'ApplicationContext.Restaurant'  is null.");
          }
            ApplicationUser applicationUser = new ApplicationUser();
            Claim claim;

            _context.Restaurant.Add(restaurant);
            _context.SaveChanges();
            applicationUser.RestaurantId = restaurant.Id;
            applicationUser.Email = "abc@def.com";
            applicationUser.Name = "RestaurantAdministrator";
            applicationUser.PhoneNumber = "1112223344";
            applicationUser.RegisterDate = DateTime.Today;
            applicationUser.StateId = 1;
            applicationUser.UserName = "RestaurantAdministrator" + restaurant.Id.ToString();
            _userManager.CreateAsync(applicationUser, "RestaurantAdminPass123!");
            _userManager.AddToRoleAsync(applicationUser, "RestaurantAdministrator");

            claim = new Claim("RestaurantId", restaurant.Id.ToString());
            _userManager.AddClaimAsync(applicationUser, claim).Wait();

            return Ok();
        }

        // DELETE: api/Restaurant/5
        [Authorize(Roles = "CompanyAdministrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            if (_context.Restaurant == null)
            {
                return NotFound();
            }
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurant.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return (_context.Restaurant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
