using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Pubela
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Tip { get; set; }

        [Required]
        public string Adresa { get; set; }

        [ForeignKey("Cetatean")]
        public int CetateanId { get; set; }
        [ValidateNever]
        public Cetatean Cetatean { get; set; }
    }
}
