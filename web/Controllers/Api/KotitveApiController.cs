using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Filters;
namespace web.Controllers_Api
{
    [Route("api/v1/Kotitve")]
    [ApiController]
    [ApiKeyAuth]
    public class KotitveApiController : ControllerBase
    {
        private readonly eShepherdContext _context;

        public KotitveApiController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: api/KotitveApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kotitev>>> GetKotitve()
        {
            return await _context.Kotitve.Include(k => k.jagenjcki).ToListAsync();
        }

        // GET: api/KotitveApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kotitev>> GetKotitev(int id)
        {
            var kotitev = await _context.Kotitve
                            .Include(k => k.jagenjcki).FirstOrDefaultAsync(m => m.kotitevID == id);

            if (kotitev == null)
            {
                return NotFound();
            }

            return kotitev;
        }

        // PUT: api/KotitveApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKotitev(int id, Kotitev kotitev)
        {
            if (id != kotitev.kotitevID)
            {
                return BadRequest();
            }

            _context.Entry(kotitev).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KotitevExists(id))
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

        // POST: api/KotitveApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Kotitev>> PostKotitev(Kotitev kotitev)
        {
            _context.Kotitve.Add(kotitev);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKotitev", new { id = kotitev.kotitevID }, kotitev);
        }

        // DELETE: api/KotitveApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kotitev>> DeleteKotitev(int id)
        {
            var kotitev = await _context.Kotitve.FindAsync(id);
            if (kotitev == null)
            {
                return NotFound();
            }

            _context.Kotitve.Remove(kotitev);
            await _context.SaveChangesAsync();

            return kotitev;
        }

        private bool KotitevExists(int id)
        {
            return _context.Kotitve.Any(e => e.kotitevID == id);
        }
    }
}
