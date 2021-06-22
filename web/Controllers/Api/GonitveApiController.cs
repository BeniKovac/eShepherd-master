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
    [Route("api/v1/Gonitve")]
    [ApiController]
    [ApiKeyAuth]
    public class GonitveApiController : ControllerBase
    {
        private readonly eShepherdContext _context;

        public GonitveApiController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: api/GonitveApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gonitev>>> GetGonitve()
        {
            return await _context.Gonitve.ToListAsync();
        }

        // GET: api/GonitveApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gonitev>> GetGonitev(int id)
        {
            var gonitev = await _context.Gonitve.FindAsync(id);

            if (gonitev == null)
            {
                return NotFound();
            }

            return gonitev;
        }

        // PUT: api/GonitveApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGonitev(int id, Gonitev gonitev)
        {
            if (id != gonitev.GonitevID)
            {
                return BadRequest();
            }

            _context.Entry(gonitev).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GonitevExists(id))
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

        // POST: api/GonitveApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Gonitev>> PostGonitev(Gonitev gonitev)
        {
            _context.Gonitve.Add(gonitev);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GonitevExists(gonitev.GonitevID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGonitev", new { id = gonitev.GonitevID }, gonitev);
        }

        // DELETE: api/GonitveApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gonitev>> DeleteGonitev(int id)
        {
            var gonitev = await _context.Gonitve.FindAsync(id);
            if (gonitev == null)
            {
                return NotFound();
            }

            _context.Gonitve.Remove(gonitev);
            await _context.SaveChangesAsync();

            return gonitev;
        }

        private bool GonitevExists(int id)
        {
            return _context.Gonitve.Any(e => e.GonitevID == id);
        }
    }
}
