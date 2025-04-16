using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Controllers
{
  //  [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CitizenController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CitizenController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_context.Citizens.ToList());

        [HttpPost]
        public IActionResult Post([FromBody] Citizen citizen)
        {
            _context.Citizens.Add(citizen);
            _context.SaveChanges();
            return Ok(citizen);
        }
    }
}
