namespace Hochwaerts.Models;

public class Verleihobjekt
{
    public int Id { get; set; }
    public int StatusId { get; set; }
    public Status? Status { get; set; }
    public Bibliothek? Bibliothek { get; set; } = null!;
    public int BibliothekId { get; set; }
    public int BuchID { get; set; } //required one to one
    public Buch? Buch { get; set; } = null!;
    public int? ZaubererId { get; set; }
    public Zauberer? Zauberer { get; set; }
    public DateTime? Verleihdatum { get; set; }
}