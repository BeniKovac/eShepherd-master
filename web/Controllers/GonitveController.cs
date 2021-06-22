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
    public class GonitveController : Controller
    {
        private readonly eShepherdContext _context;

        public GonitveController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: Gonitve
        public async Task<IActionResult> Index(
                                string sortOrder,
                                string currentFilter,
                                string searchString,
                                int? pageNumber)
        {
                ViewData["CurrentSort"] = sortOrder;
                ViewData["IDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ID_desc" : "";
                ViewData["GonitevDatumSortParm"] = sortOrder == "GonitevDatum" ? "gonitevdatum_desc" : "GonitevDatum";

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewData["CurrentFilter"] = searchString;
                var gonitve = from g in _context.Gonitve
                                select g;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        gonitve = gonitve.Where(g => g.OvcaID.Contains(searchString));
                    }

                switch (sortOrder)
                {
                    case "ID_desc":
                        gonitve = gonitve.OrderByDescending(g => g.ovca.OvcaID);
                        break;
                    case "GonitevDatum":
                        gonitve = gonitve.OrderBy(g => g.DatumGonitve);
                        break;
                    case "gonitevdatum_desc":
                        gonitve = gonitve.OrderByDescending(g => g.DatumGonitve);
                        break;
                    default:
                        gonitve = gonitve.OrderBy(g => g.ovca.OvcaID);
                        break;
                }
                int pageSize = 10;
                return View(await PaginatedList<Gonitev>.CreateAsync(gonitve.AsNoTracking(), pageNumber ?? 1, pageSize));
            }

        // GET: Gonitve/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gonitev = await _context.Gonitve
                .Include(g => g.ovca)
                .Include(g => g.oven)
                .FirstOrDefaultAsync(m => m.GonitevID == id);
            if (gonitev == null)
            {
                return NotFound();
            }

            return View(gonitev);
        }

        // GET: Gonitve/Create
        public IActionResult Create()
        {
            ViewData["OvcaID"] = new SelectList(_context.Ovce, "OvcaID", "OvcaID");
            ViewData["OvenID"] = new SelectList(_context.Ovni, "OvenID", "OvenID");
            return View();
        }

        // POST: Gonitve/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GonitevID,DatumGonitve,OvcaID,OvenID,PredvidenaKotitev,Opombe")] Gonitev gonitev)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gonitev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OvcaID"] = new SelectList(_context.Ovce, "OvcaID", "OvcaID", gonitev.OvcaID);
            ViewData["OvenID"] = new SelectList(_context.Ovni, "OvenID", "OvenID", gonitev.OvenID);
            return View(gonitev);
        }

        // GET: Gonitve/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gonitev = await _context.Gonitve.FindAsync(id);
            if (gonitev == null)
            {
                return NotFound();
            }
            ViewData["OvcaID"] = new SelectList(_context.Ovce, "OvcaID", "OvcaID", gonitev.OvcaID);
            ViewData["OvenID"] = new SelectList(_context.Ovni, "OvenID", "OvenID", gonitev.OvenID);
            return View(gonitev);
        }

        // POST: Gonitve/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GonitevID,DatumGonitve,OvcaID,OvenID,PredvidenaKotitev,Opombe")] Gonitev gonitev)
        {
            if (id != gonitev.GonitevID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gonitev);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GonitevExists(gonitev.GonitevID))
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
            ViewData["OvcaID"] = new SelectList(_context.Ovce, "OvcaID", "OvcaID", gonitev.OvcaID);
            ViewData["OvenID"] = new SelectList(_context.Ovni, "OvenID", "OvenID", gonitev.OvenID);
            return View(gonitev);
        }

        // GET: Gonitve/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gonitev = await _context.Gonitve
                .Include(g => g.ovca)
                .Include(g => g.oven)
                .FirstOrDefaultAsync(m => m.GonitevID == id);
            if (gonitev == null)
            {
                return NotFound();
            }

            return View(gonitev);
        }

        // POST: Gonitve/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gonitev = await _context.Gonitve.FindAsync(id);
            _context.Gonitve.Remove(gonitev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GonitevExists(int id)
        {
            return _context.Gonitve.Any(e => e.GonitevID == id);
        }
    }
}
