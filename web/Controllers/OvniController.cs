using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace web.Controllers
{
    [Authorize]
    public class OvniController : Controller
    {
        private readonly eShepherdContext _context;

        public OvniController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: Ovni
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["IDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ID_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Datum" ? "datum_desc" : "Datum";
            ViewData["CurrentFilter"] = searchString;   
           // var ovce = _context.Ovce.Include(o => o.creda).Include(o => o.mama).Include(o => o.oce);
            var ovni = from o in _context.Ovni.Include(o => o.creda)
                .Include(o => o.vseKotitve)
                .Include(o => o.vseGonitve)
                 select o;
           if (!String.IsNullOrEmpty(searchString))
          {
               ovni = ovni.Where(s => s.OvenID.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ID_desc":
                    ovni = ovni.OrderByDescending(o => o.OvenID);
                    break;
                case "Datum":
                    ovni =  ovni.OrderBy(o => o.DatumRojstva);
                    break;
                 case "datum_desc":
                    ovni = ovni.OrderByDescending(o => o.DatumRojstva);
                    break;
                default:
                    ovni = ovni.OrderBy(o => o.OvenID);
                    break;
            }
            return View(await ovni.ToListAsync());

        }

        // GET: Ovni/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oven = await _context.Ovni
                .Include(o => o.creda)
                .Include(o => o.vseKotitve)
                .Include(o => o.vseGonitve)
                .FirstOrDefaultAsync(m => m.OvenID == id);
            if (oven == null)
            {
                return NotFound();
            }

            return View(oven);
        }

        // GET: Ovni/Create
        public IActionResult Create()
        {
            ViewData["CredaID"] = new SelectList(_context.Crede, "CredeID", "CredeID");
            return View();
        }

        // POST: Ovni/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OvenID,CredaID,mamaID, oceID,DatumRojstva,Pasma,SteviloSorojencev,Stanje,Opombe,Poreklo")] Oven oven)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oven);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CredaID"] = new SelectList(_context.Crede, "CredeID", "CredeID", oven.CredaID);
            return View(oven);
        }


       public async Task<IActionResult> MoveTo0(string id, Oven oven)
        {
            oven.CredaID = "0";
            _context.Update(oven);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = id, saveChangesError = true });
        }

        // GET: Ovni/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oven = await _context.Ovni.FindAsync(id);
            if (oven == null)
            {
                return NotFound();
            }
            ViewData["CredaID"] = new SelectList(_context.Crede, "CredeID", "CredeID", oven.CredaID);
            return View(oven);
        }

        // POST: Ovni/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OvenID,CredaID,mamaID, oceID,DatumRojstva,Pasma,OvcaID,SteviloSorojencev,Stanje,Opombe,Poreklo")] Oven oven)
        {
            if (id != oven.OvenID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oven);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OvenExists(oven.OvenID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CredaID"] = new SelectList(_context.Crede, "CredeID", "CredeID", oven.CredaID);
            return View(oven);
        }

        // GET: Ovni/Delete/5
        public async Task<IActionResult> Delete(string id,  bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oven = await _context.Ovni
                .Include(o => o.creda)
                .FirstOrDefaultAsync(m => m.OvenID == id);
            if (oven == null)
            {
                return NotFound();
            }
                if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(oven);
        }

        // POST: Ovni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var oven = await _context.Ovni.FindAsync(id);

            if (oven == null) {
                return RedirectToAction(nameof(Index));
            }
            try {
                _context.Ovni.Remove(oven);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */) {

            return RedirectToAction(nameof(Index), new { id = id, saveChangesError = true });
            }
        }

        private bool OvenExists(string id)
        {
            return _context.Ovni.Any(e => e.OvenID == id);
        }
    }
}
