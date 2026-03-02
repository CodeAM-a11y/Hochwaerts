namespace Hochwaerts.Models;

public class Bibliothek
{
    public int Id { get; set; }
    public ICollection<Verleihobjekt> Verleihobjekte { get; } = new List<Verleihobjekt>();
}