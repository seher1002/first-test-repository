using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SensorData> SensorReadings { get; set; }
        public DbSet<RfidTag> RfidTags { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Pubela> Pubele { get; set; }

    }
}
