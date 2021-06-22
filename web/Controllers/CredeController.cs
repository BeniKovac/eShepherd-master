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

namespace web.Models.eShepherdViewModels
{
     [Authorize]
    public class CredeController : Controller
    {
        private readonly eShepherdContext _context;

        public CredeController(eShepherdContext context)
        {
            _context = context;
        }

        // GET: Crede
        public async Task<IActionResult> Index(String credaID)
        {
            var viewModel = new CredeIndexData();
            viewModel.Crede = await _context.Crede
                            .Include(c => c.SeznamOvac)
                            .ToListAsync();
                if (credaID != null)
                {
                    ViewData["CredeID"] = credaID;
                    Creda creda = viewModel.Crede.Where(
                        c => c.CredeID.Equals(credaID)).Single();
                        viewModel.Ovce = creda.SeznamOvac;
                }
             


            return View(viewModel);
   
         
        }

        // GET: Crede/Details/5
        public async Task<IActionResult> Details(String? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creda = await _context.Crede
            .Include(k => k.SeznamOvac)
            .FirstOrDefaultAsync(m => m.CredeID.Equals(id.ToString()));
            if (creda == null)
            {
                return NotFound();
            }

            return View(creda);
        }

        // GET: Crede/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crede/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CredeID,SteviloOvc,Opombe")] Creda creda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creda);
        }

        // GET: Crede/Edit/5
        public async Task<IActionResult> Edit(String? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creda = await _context.Crede.FindAsync(id);
            if (creda == null)
            {
                return NotFound();
            }
            return View(creda);
        }

        // POST: Crede/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String id, [Bind("CredeID,SteviloOvc,Opombe")] Creda creda)
        {
            if (id !=creda.CredeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredaExists(creda.CredeID))
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
            return View(creda);
        }

        // GET: Crede/Delete/5
        public async Task<IActionResult> Delete(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creda = await _context.Crede
                .FirstOrDefaultAsync(m => m.CredeID == (id));
            if (creda == null)
            {
                return NotFound();
            }

            return View(creda);
        }

        // POST: Crede/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(String id)
        {
            var creda = await _context.Crede.FindAsync(id);
            _context.Crede.Remove(creda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredaExists(String id)
        {
            return _context.Crede.Any(e => e.CredeID == id);
        }
    }
}
