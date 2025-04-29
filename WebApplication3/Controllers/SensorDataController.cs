using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [Route("api/sensor")]  // 🔹 Endpoint-ul API va fi la /api/sensor
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SensorDataController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Endpoint pentru primirea datelor
        [HttpPost]
        public async Task<IActionResult> PostSensorData([FromBody] SensorData data)
        {
            if (data == null || string.IsNullOrEmpty(data.TagId))
                return BadRequest("Invalid data.");

            data.Timestamp = DateTime.UtcNow; // Setează automat timestamp-ul
            _context.SensorData.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSensorData), new { id = data.Id }, data);
        }

        // 🔹 Endpoint pentru obținerea datelor salvate
        [HttpGet]
        public async Task<IActionResult> GetSensorData()
        {
            var data = await _context.SensorData.ToListAsync();
            return Ok(data);
        }
    }
}
