

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Cetatean
    {
        public int Id { get; set; }

        [Required]
        public string Nume { get; set; }

        [Required]
        public string Prenume { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CNP { get; set; }

        // Relație cu Pubele (dacă o folosești)
        public ICollection<Pubela> Pubele { get; set; }

        public Cetatean()
        {
            Pubele = new List<Pubela>();
        }
    }
}
