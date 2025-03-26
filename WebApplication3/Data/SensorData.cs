using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class SensorData
    {
        [Key]
        public int Id { get; set; }  // Index (ID-ul unic în baza de date)

        [Required]
        public string TagId { get; set; } = string.Empty;  // ID-ul tag-ului RFID

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;  // Timpul citirii
    }
}
