using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web.Models;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models.eShepherdViewModels;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly eShepherdContext _context;

        public HomeController(eShepherdContext context)
        {
            _context = context;
        }
                public async Task<ActionResult> About()
        {
            IQueryable<JagenjckiGroup> data = 
                from jagenjcek in _context.Jagenjcki
                group jagenjcek by jagenjcek.kotitevID.ToString() into Idgroup
                select new JagenjckiGroup()
                {
                    kotitevID = Idgroup.Key,
                    JagenjckiCount = Idgroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }
        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
