using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class RfidTag
    {
        public int Id { get; set; }
        public string TagId { get; set; } = string.Empty; // 🔹 Inițializează cu string gol
        public string Name { get; set; } = string.Empty;
    }
}
