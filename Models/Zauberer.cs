namespace Hochwaerts.Models;

public class Zauberer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Haus { get; set; }
    public ICollection<Buch> Bücher { get; } = new List<Buch>(); //required one to many
}