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
using web.Models.eShepherdViewModels;
namespace web.Controllers
{
    [Authorize]
    public class OvceController : Controller
    {
        private readonly eShepherdContext _context;

        public OvceController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: Ovce3
        public async Task<IActionResult> Index(
                                    string sortOrder,
                                    string currentFilter,
                                    string searchString,
                                    int? pageNumber, int? ovcaID)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ID_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Datum" ? "datum_desc" : "Datum";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;   
           // var ovce = _context.Ovce.Include(o => o.creda).Include(o => o.mama).Include(o => o.oce);
            var ovce = from o in _context.Ovce.Include(o => o.creda)
                .Include(o => o.mama)
                .Include(o => o.oce)
                .Include(o => o.SeznamKotitev)
                .Include(o => o.SeznamGonitev)
                 select o;
           if (!String.IsNullOrEmpty(searchString))
          {
               ovce = ovce.Where(s => s.OvcaID.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ID_desc":
                    ovce = ovce.OrderByDescending(o => o.OvcaID);
                    break;
                case "Datum":
                    ovce = ovce.OrderBy(o => o.DatumRojstva);
                    break;
                 case "datum_desc":
                    ovce = ovce.OrderByDescending(o => o.DatumRojstva);
                    break;
                default:
                    ovce = ovce.OrderBy(o => o.OvcaID);
                    break;
            }

            int pageSize = 10;
            var novModel = new OvceIndexData();
            novModel.Ovce = await PaginatedList<Ovca>.CreateAsync(ovce.AsNoTracking(), pageNumber ?? 1, pageSize);
        
        if(ovcaID != null){
            var ovca = await _context.Ovce
                .Include(k => k.SeznamKotitev)
                    .ThenInclude(k => k.jagenjcki)
                .Include(k => k.SeznamGonitev)
                .FirstOrDefaultAsync(m => m.OvcaID == ovcaID.ToString());
                novModel.Kotitve = ovca.SeznamKotitev;
                novModel.Gonitve = ovca.SeznamGonitev;
        }
            return View(novModel);
        }

        // GET: Ovce3/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ovca = await _context.Ovce
                .Include(o => o.creda)
                .Include(o => o.mama)
                .Include(o => o.oce)
                .Include(o => o.SeznamKotitev)
                .Include(o => o.SeznamGonitev)
                .FirstOrDefaultAsync(m => m.OvcaID == id);
            if (ovca == null)
            {
                return NotFound();
            }

            return View(ovca);
        }

        // GET: Ovce3/Create
        public IActionResult Create()
        {
            ViewData["CredaID"] = new SelectList(_context.Crede, "CredeID", "CredeID");
            ViewData["mamaID"] = new SelectList(_context.Ovce, "OvcaID", "OvcaID");
            ViewData["oceID"] = new SelectList(_context.Ovni, "OvenID", "OvenID");

            return View();
        }

        // POST: Ovce3/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OvcaID,CredaID,DatumRojstva,Pasma,mamaID,oceID,SteviloSorojencev,Stanje,Opombe,")] Ovca ovca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ovca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CredaID"] = new SelectList(_context.Crede, "CredeID", "CredeID", ovca.CredaID);
            ViewData["mamaID"] = new SelectList(_context.Ovce, "OvcaID", "OvcaID", ovca.mamaID);
            ViewData["oceID"] = new SelectList(_context.Ovni, "OvenID", "OvenID", ovca.oceID);

            return View(ovca);
        }

       public async Task<IActionResult> MoveTo0(string id, Ovca ovca)
        {
            ovca.CredaID = "0";
            _context.Update(ovca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = id, saveChangesError = true });
        }

        // GET: Ovce3/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ovca = await _context.Ovce.FindAsync(id);
            if (ovca == null)
            {
                return NotFound();
            }
            ViewData["CredaID"] = new SelectList(_context.Crede, "CredeID", "CredeID", ovca.CredaID);
            ViewData["mamaID"] = new SelectList(_context.Ovce, "OvcaID", "OvcaID", ovca.mamaID);
            ViewData["oceID"] = new SelectList(_context.Ovni, "OvenID", "OvenID", ovca.oceID);

            return View(ovca);
        }

        // POST: Ovce3/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OvcaID,CredaID,DatumRojstva,Pasma, mamaID,oceID, SteviloSorojencev,Stanje,Opombe,SteviloKotitev,PovprecjeJagenjckov")] Ovca ovca)
        {
            if (id != ovca.OvcaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ovca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OvcaExists(ovca.OvcaID))
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
            ViewData["CredaID"] = new SelectList(_context.Crede, "CredeID", "CredeID", ovca.CredaID);
            ViewData["mamaID"] = new SelectList(_context.Ovce, "OvcaID", "OvcaID", ovca.mamaID);
            ViewData["oceID"] = new SelectList(_context.Ovni, "OvenID", "OvenID", ovca.oceID);

            return View(ovca);
        }

        // GET: Ovce3/Delete/5
        public async Task<IActionResult> Delete(string id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ovca = await _context.Ovce
                .Include(o => o.creda)
                .Include(o => o.mama)
                .Include(o => o.oce)
                .FirstOrDefaultAsync(m => m.OvcaID == id);
            if (ovca == null)
            {
                return NotFound();
            }
                if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }


            return View(ovca);
        }

        // POST: Ovce3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ovca = await _context.Ovce.FindAsync(id);

            if (ovca == null) {
                return RedirectToAction(nameof(Index));
            }
            try {
                _context.Ovce.Remove(ovca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */) {

            return RedirectToAction(nameof(Index), new { id = id, saveChangesError = true });
            }
        }

        private bool OvcaExists(string id)
        {
            return _context.Ovce.Any(e => e.OvcaID == id);
        }
    }
}
