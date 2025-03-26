//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using WebApplication3.Data;
//using WebApplication3.Models;

//namespace WebApplication3.Controllers
//{
//    public class PubelaController : Controller
//    {
//        private readonly AppDbContext _context;

//        public PubelaController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Pubela
//        public async Task<IActionResult> Index()
//        {
//            var pubele = _context.Pubele.Include(p => p.Cetatean);
//            return View(await pubele.ToListAsync());
//        }

//        // GET: Pubela/Create
//        public IActionResult Create()
//        {
//            ViewData["CetateanId"] = new SelectList(_context.Citizens, "Id", "Nume");
//            return View();
//        }

//        // POST: Pubela/Create
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Create([Bind("Id,Tip,Adresa,CetateanId")] Pubela pubela)
//        //{
//        //    Console.WriteLine("AM AJUNS ÎN CONTROLLER!");

//        //    if (ModelState.IsValid)
//        //    {
//        //        _context.Add(pubela);
//        //        await _context.SaveChangesAsync();
//        //        return RedirectToAction(nameof(Index));
//        //    }
//        //    ViewData["CetateanId"] = new SelectList(_context.Citizens, "Id", "Nume", pubela.CetateanId);
//        //    return View(pubela);
//        //}
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Tip,Adresa,CetateanId")] Pubela pubela)
//        {
//            Console.WriteLine(">>> A ajuns în [HttpPost] Create");

//            if (ModelState.IsValid)
//            {
//                Console.WriteLine(">>> ModelState VALID. Se salvează.");
//                _context.Add(pubela);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            else
//            {
//                Console.WriteLine(">>> ModelState INVALID.");
//                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
//                {
//                    Console.WriteLine(">>> EROARE: " + error.ErrorMessage);
//                }
//            }

//            ViewData["CetateanId"] = new SelectList(_context.Citizens, "Id", "Nume", pubela.CetateanId);
//            return View(pubela);
//        }

//    }
//}



using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class PubelaController : Controller
    {
        private readonly AppDbContext _context;

        public PubelaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pubela
        public async Task<IActionResult> Index()
        {
            var pubele = _context.Pubele.Include(p => p.Cetatean);
            return View(await pubele.ToListAsync());
        }

        // GET: Pubela/Create
        public IActionResult Create()
        {
            ViewData["CetateanId"] = new SelectList(_context.Citizens, "Id", "Nume");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tip,Adresa,CetateanId")] Pubela pubela)
        {
            Console.WriteLine(">>> A ajuns în POST Create");
            Console.WriteLine(">>> Tip: " + pubela.Tip);
            Console.WriteLine(">>> Adresa: " + pubela.Adresa);
            Console.WriteLine(">>> CetateanId: " + pubela.CetateanId);

            if (!ModelState.IsValid)
            {
                Console.WriteLine(">>> ModelState INVALID!");
                foreach (var err in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(">>> EROARE: " + err.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine(">>> ModelState VALID! Se salvează!");
                _context.Pubele.Add(pubela);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CetateanId"] = new SelectList(_context.Citizens, "Id", "Nume", pubela.CetateanId);
            return View(pubela);
        }

        // POST: Pubela/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Tip,Adresa,CetateanId")] Pubela pubela)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Pubele.Add(pubela);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["CetateanId"] = new SelectList(_context.Citizens, "Id", "Nume", pubela.CetateanId);
        //    return View(pubela);
        //}
    }
}

