using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class LocationPoint
    {
        [Key] // Cheie primară
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
