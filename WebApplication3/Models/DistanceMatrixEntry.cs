namespace WebApplication3.Models
{
    public class DistanceMatrixEntry
    {
        public int Id { get; set; }
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
        public double Distance { get; set; }
    }
}
