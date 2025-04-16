


using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class MapController : Controller
    {
        private readonly AppDbContext _context;

        public MapController(AppDbContext context)
        {
            _context = context;
        }

        // 🔄 1. Acțiune de importare din Excel
        public IActionResult ImportFromExcel()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "Traseu 3 - SB 99 ULB .xlsx");
            var points = ExcelReader.ReadRoute(path);

            if (!_context.LocationPoints.Any())
            {
                _context.LocationPoints.AddRange(points);
                _context.SaveChanges();
                return Content("Punctele au fost importate cu succes.");
            }
            else
            {
                return Content("Punctele există deja în baza de date.");
            }
        }

        // 🗺️ 2. Afișează harta folosind baza de date
        public IActionResult Index()
        {
            var points = _context.LocationPoints.ToList();
            return View(points);
        }
    }
}
