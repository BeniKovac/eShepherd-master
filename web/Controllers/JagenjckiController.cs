using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class JagenjckiController : Controller
    {
        private readonly eShepherdContext _context;

        public JagenjckiController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: Jagenjcki
        public async Task<IActionResult> Index(
                                string sortOrder,
                                string currentFilter,
                                string searchString,
                                int? pageNumber)
        {
                ViewData["CurrentSort"] = sortOrder;
                ViewData["IDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ID_desc" : "";
                ViewData["kotitevIDSortParm"] = sortOrder == "kotitevID" ? "kotitevID_desc" : "kotitevID";

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewData["CurrentFilter"] = searchString;
                var jagenjcki = from j in _context.Jagenjcki.Include(j => j.kotitev).AsNoTracking() select j;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        jagenjcki = jagenjcki.Where(j => j.IdJagenjcka.Contains(searchString)
                                            || j.kotitevID.ToString().Contains(searchString));
                    }

                switch (sortOrder)
                {
                    case "ID_desc":
                        jagenjcki = jagenjcki.OrderByDescending(j => j.IdJagenjcka);
                        break;
                    case "kotitevID_desc":
                        jagenjcki = jagenjcki.OrderBy(j => j.kotitevID);
                        break;
                    case "kotitevID":
                        jagenjcki = jagenjcki.OrderBy(j => j.kotitevID);
                        break;
                    default:
                        jagenjcki = jagenjcki.OrderBy(j => j.IdJagenjcka);
                        break;
                }

            int maxID = -1;
            foreach(Kotitev kot in _context.Kotitve){
                if(kot.kotitevID > maxID){
                    maxID = kot.kotitevID;
                }
            }
            ViewBag.LastKotitevID = maxID;
            
                int pageSize = 10;
                return View(await PaginatedList<Jagenjcek>.CreateAsync(jagenjcki.AsNoTracking(), pageNumber ?? 1, pageSize));
            }

        // GET: Jagenjcki/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jagenjcek = await _context.Jagenjcki
                .Include(j => j.kotitev)
                .FirstOrDefaultAsync(m => m.skritIdJagenjcka == id);

            if (jagenjcek == null)
            {
                return NotFound();
            }

            return View(jagenjcek);
        }

        // GET: Jagenjcki/Create

        public IActionResult Create(int ID)
        {
            var jagenjcek = new Jagenjcek();
            jagenjcek.kotitevID = ID;

            var datetime = DateTime.Now;
            var Idmame = "str";
            foreach(Kotitev kot in _context.Kotitve){
                if(kot.kotitevID == ID){
                    datetime = kot.DatumKotitve;
                    Idmame = kot.OvcaID;
                }
            }
            TempData["Datum"] = datetime.ToShortDateString();
            TempData["Mama"] = Idmame;

            return View(jagenjcek);
        }

        // POST: Jagenjcki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("skritIdJagenjcka,IdJagenjcka,kotitevID,spol, stanje, opombe")] Jagenjcek jagenjcek, int kotitevID)
        {

            if (ModelState.IsValid)
            {
                _context.Add(jagenjcek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(jagenjcek);
        }

        // GET: Jagenjcki/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jagenjcek = await _context.Jagenjcki.FindAsync(id);
            if (jagenjcek == null)
            {
                return NotFound();
            }
            ViewData["kotitevID"] = new SelectList(_context.Kotitve, "kotitevID", "kotitevID", jagenjcek.kotitevID);
            return View(jagenjcek);
        }

        // POST: Jagenjcki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("skritIdJagenjcka,IdJagenjcka,kotitevID,spol, stanje, opombe")] Jagenjcek jagenjcek)
        {
            if (id != jagenjcek.skritIdJagenjcka)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jagenjcek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JagenjcekExists(jagenjcek.IdJagenjcka))
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
            ViewData["kotitevID"] = new SelectList(_context.Kotitve, "kotitevID", "kotitevID", jagenjcek.kotitevID);
            return View(jagenjcek);
        }

        // GET: Jagenjcki/Delete/5
        public async Task<IActionResult> Delete(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jagenjcek = await _context.Jagenjcki
                .Include(j => j.IdJagenjcka)
                .Include(j => j.skritIdJagenjcka)
                .Include(j => j.kotitev)
                .Include(j => j.spol)
                .FirstOrDefaultAsync(m => m.IdJagenjcka == id);
            if (jagenjcek == null)
            {
                return NotFound();
            }

            return View(jagenjcek);
        }

        // POST: Jagenjcki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(String id)
        {
            var jagenjcek = await _context.Jagenjcki.FindAsync(id);
            _context.Jagenjcki.Remove(jagenjcek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JagenjcekExists(String id)
        {
            return _context.Jagenjcki.Any(e => e.IdJagenjcka == id);
        }
    }
}
