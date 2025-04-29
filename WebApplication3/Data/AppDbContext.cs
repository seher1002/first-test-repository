


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /
        public DbSet<Cetatean> Citizens { get; set; }
        public DbSet<Pubela> Pubele { get; set; }
        public DbSet<SensorData> SensorData { get; set; }

        public DbSet<LocationPoint> LocationPoints { get; set; }

        public DbSet<DistanceMatrixEntry> DistanceMatrix { get; set; }

        public DbSet<Distance> Distances { get; set; }

       // public DbSet<LocationPoint> LocationPoints { get; set; }



    }
}
