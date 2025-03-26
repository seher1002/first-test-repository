namespace WebApplication3.Models
{
    public class Citizen
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string CNP { get; set; }
        public ICollection<Pubela> Pubele { get; set; }

    }
}
