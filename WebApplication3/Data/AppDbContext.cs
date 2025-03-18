using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;  // Asigură-te că ai namespace-ul corect

namespace YourNamespace.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SensorData> SensorReadings { get; set; }  // Datele primite de la senzori
        public DbSet<RfidTag> RfidTags { get; set; }  // 🔹 Adaugă RfidTags aici
    }
}
