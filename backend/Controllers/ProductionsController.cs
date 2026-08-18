using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCms.Api.Data;
using ShoppingCms.Api.Models;

namespace ShoppingCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Production>>> GetProductions()
        {
            return await _context.Productions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Production>> GetProduction(int id)
        {
            var production = await _context.Productions.FindAsync(id);

            if (production == null)
            {
                return NotFound();
            }

            return production;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduction(int id, Production production)
        {
            if (id != production.Id)
            {
                return BadRequest();
            }

            _context.Entry(production).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Production>> PostProduction(Production production)
        {
            _context.Productions.Add(production);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduction", new { id = production.Id }, production);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduction(int id)
        {
            var production = await _context.Productions.FindAsync(id);
            if (production == null)
            {
                return NotFound();
            }

            _context.Productions.Remove(production);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductionExists(int id)
        {
            return _context.Productions.Any(e => e.Id == id);
        }
    }
}
