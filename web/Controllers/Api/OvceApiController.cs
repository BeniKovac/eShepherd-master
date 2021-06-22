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
    [Route("api/v1/Ovce")]
    [ApiController]
    [ApiKeyAuth]
    public class OvceApiController : ControllerBase
    {
        private readonly eShepherdContext _context;

        public OvceApiController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: api/OvceApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ovca>>> GetOvce()
        {
            return await _context.Ovce
            .Include(o => o.SeznamKotitev)
            .Include(o => o.SeznamGonitev)
            .ToListAsync();
        }

        // GET: api/OvceApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ovca>> GetOvca(string id)
        {
            var ovca = await _context.Ovce
                                .Include(c => c.SeznamKotitev)
                                .Include(g => g.SeznamGonitev).FirstOrDefaultAsync(n => n.OvcaID == id);

            if (ovca == null)
            {
                return NotFound();
            }

            return ovca;
        }

        // PUT: api/OvceApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOvca(string id, Ovca ovca)
        {
            if (id != ovca.OvcaID)
            {
                return BadRequest();
            }

            _context.Entry(ovca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OvcaExists(id))
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

        // POST: api/OvceApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ovca>> PostOvca(Ovca ovca)
        {
            _context.Ovce.Add(ovca);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OvcaExists(ovca.OvcaID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOvca", new { id = ovca.OvcaID }, ovca);
        }

        // DELETE: api/OvceApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ovca>> DeleteOvca(string id)
        {
            var ovca = await _context.Ovce.FindAsync(id);
            if (ovca == null)
            {
                return NotFound();
            }

            _context.Ovce.Remove(ovca);
            await _context.SaveChangesAsync();

            return ovca;
        }

        private bool OvcaExists(string id)
        {
            return _context.Ovce.Any(e => e.OvcaID == id);
        }
    }
}
