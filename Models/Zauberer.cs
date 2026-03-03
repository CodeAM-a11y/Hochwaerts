namespace Hochwaerts.Models;

public class Zauberer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Haus { get; set; }
    public ICollection<Verleihobjekt> Verleihobjekte { get; } = new List<Verleihobjekt>(); //required one to many
}