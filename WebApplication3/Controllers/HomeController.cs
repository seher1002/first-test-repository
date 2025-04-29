using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Pagina principală
    public IActionResult Index()
    {
        var points = _context.LocationPoints.ToList(); 
        return View(points); 
    }

    // GET: Afișează datele senzorilor
    [HttpGet]
    public IActionResult SensorData()
    {
        var data = _context.SensorData.ToList();
        return View(data);
    }

    // POST: Primește date noi despre senzori (ex: din Postman)
    [HttpPost]
    public IActionResult SensorData([FromBody] SensorData data)
    {
        Console.WriteLine("🔧 [POST] SensorData primit: " + data?.TagId);

        if (ModelState.IsValid)
        {
            data.Timestamp = DateTime.Now; // completăm timestamp automat
            _context.SensorData.Add(data);
            _context.SaveChanges();
            return Ok(data); // trimitem răspuns JSON în Postman
        }

        return BadRequest(ModelState);
    }

    // GET: Afișează cetățenii
    [HttpGet]
    public IActionResult Cetateni()
    {
        var cetateni = _context.Citizens.ToList();
        return View(cetateni);
    }

    // POST: Adaugă un cetățean nou din formular
    [HttpPost]
    public IActionResult Cetateni(Cetatean citizen)
    {
        Console.WriteLine("🔔 FORMULAR TRIMIS CU: " + citizen.Nume);

        if (ModelState.IsValid)
        {
            _context.Citizens.Add(citizen);
            _context.SaveChanges();
            return RedirectToAction("Cetateni");
        }

        var cetateni = _context.Citizens.ToList();
        return View(cetateni);
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
