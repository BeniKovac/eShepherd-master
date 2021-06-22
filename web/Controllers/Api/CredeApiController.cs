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
    [Route("api/v1/Crede")]
    [ApiController]
    [ApiKeyAuth]
    public class CredeApiController : ControllerBase
    {
        private readonly eShepherdContext _context;

        public CredeApiController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: api/CredeApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creda>>> GetCrede()
        {
            return await _context.Crede
            .Include(c => c.SeznamOvac)
            .ToListAsync();
        }

        // GET: api/CredeApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Creda>> GetCreda(String id)
        {
            var creda = await _context.Crede
                    .Include(c => c.SeznamOvac).FirstOrDefaultAsync(m => m.CredeID == id);

            if (creda == null)
            {
                return NotFound();
            }

            return creda;
        }

        // PUT: api/CredeApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreda(String id, Creda creda)
        {
            if (id != creda.CredeID)
            {
                return BadRequest();
            }

            _context.Entry(creda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CredaExists(id))
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

        // POST: api/CredeApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Creda>> PostCreda(Creda creda)
        {
            _context.Crede.Add(creda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreda", new { id = creda.CredeID }, creda);
        }

        // DELETE: api/CredeApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Creda>> DeleteCreda(String id)
        {
            var creda = await _context.Crede.FindAsync(id);
            if (creda == null)
            {
                return NotFound();
            }

            _context.Crede.Remove(creda);
            await _context.SaveChangesAsync();

            return creda;
        }

        private bool CredaExists(String id)
        {
            return _context.Crede.Any(e => e.CredeID == id);
        }
    }
}
