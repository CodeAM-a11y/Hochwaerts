namespace Hochwaerts.Models;

public class Status
{
    public int Id { get; set; }
    public string Zustand { get; set; }
    public ICollection<Verleihobjekt> Verleihobjekte { get; } = new List<Verleihobjekt>();
}