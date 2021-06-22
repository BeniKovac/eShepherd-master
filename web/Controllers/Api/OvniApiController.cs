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
    [Route("api/v1/Ovni")]
    [ApiController]
    [ApiKeyAuth]
    public class OvniApiController : ControllerBase
    {
        private readonly eShepherdContext _context;

        public OvniApiController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: api/OvniApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oven>>> GetOvni()
        {
            return await _context.Ovni
            .Include(o => o.vseKotitve)
            .Include(o => o.vseGonitve)
            .ToListAsync();
        }

        // GET: api/OvniApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Oven>> GetOven(string id)
        {
            var oven = await _context.Ovni
                        .Include(c => c.vseKotitve)
                        .Include(g => g.vseGonitve).FirstOrDefaultAsync(n => n.OvenID == id);

            if (oven == null)
            {
                return NotFound();
            }

            return oven;
        }

        // PUT: api/OvniApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOven(string id, Oven oven)
        {
            if (id != oven.OvenID)
            {
                return BadRequest();
            }

            _context.Entry(oven).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OvenExists(id))
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

        // POST: api/OvniApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Oven>> PostOven(Oven oven)
        {
            _context.Ovni.Add(oven);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OvenExists(oven.OvenID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOven", new { id = oven.OvenID }, oven);
        }

        // DELETE: api/OvniApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Oven>> DeleteOven(string id)
        {
            var oven = await _context.Ovni.FindAsync(id);
            if (oven == null)
            {
                return NotFound();
            }

            _context.Ovni.Remove(oven);
            await _context.SaveChangesAsync();

            return oven;
        }

        private bool OvenExists(string id)
        {
            return _context.Ovni.Any(e => e.OvenID == id);
        }
    }
}
