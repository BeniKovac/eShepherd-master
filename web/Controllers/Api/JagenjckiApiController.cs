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
    [Route("api/v1/Jagenjcki")]
    [ApiController]
    [ApiKeyAuth]
    public class JagenjckiApiController : ControllerBase
    {
        private readonly eShepherdContext _context;

        public JagenjckiApiController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: api/JagenjckiApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jagenjcek>>> GetJagenjcki()
        {
            return await _context.Jagenjcki.ToListAsync();
        }

        // GET: api/JagenjckiApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jagenjcek>> GetJagenjcek(int id)
        {
            var jagenjcek = await _context.Jagenjcki.FindAsync(id);

            if (jagenjcek == null)
            {
                return NotFound();
            }

            return jagenjcek;
        }

        // PUT: api/JagenjckiApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJagenjcek(int id, Jagenjcek jagenjcek)
        {
            if (id != jagenjcek.skritIdJagenjcka)
            {
                return BadRequest();
            }

            _context.Entry(jagenjcek).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JagenjcekExists(id))
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

        // POST: api/JagenjckiApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Jagenjcek>> PostJagenjcek(Jagenjcek jagenjcek)
        {
            _context.Jagenjcki.Add(jagenjcek);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJagenjcek", new { id = jagenjcek.skritIdJagenjcka }, jagenjcek);
        }

        // DELETE: api/JagenjckiApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Jagenjcek>> DeleteJagenjcek(int id)
        {
            var jagenjcek = await _context.Jagenjcki.FindAsync(id);
            if (jagenjcek == null)
            {
                return NotFound();
            }

            _context.Jagenjcki.Remove(jagenjcek);
            await _context.SaveChangesAsync();

            return jagenjcek;
        }

        private bool JagenjcekExists(int id)
        {
            return _context.Jagenjcki.Any(e => e.skritIdJagenjcka == id);
        }
    }
}
