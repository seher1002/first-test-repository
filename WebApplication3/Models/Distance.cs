using System.ComponentModel.DataAnnotations;

public class Distance
{
    [Key]
    public int Id { get; set; }
    public int FromPointId { get; set; }
    public int ToPointId { get; set; }
    public double DistanceMeters { get; set; }
}
