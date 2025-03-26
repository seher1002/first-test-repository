using System;

namespace WebApplication3.Models
{
    public class RfidTag
    {
        public int Id { get; set; }
        public string TagId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
