namespace Hochwaerts.Models;

public class Buch
{
    public int Id { get; set; }

    public string Titel { get; set; }

    public string Inhalt { get; set; }

    // 1:n Beziehung → Ein Buch hat viele Autoren
    public List<Autor> Autoren { get; set; } = new();

    public DateTime Erscheinungsjahr { get; set; }

    public string ISBN { get; set; }

    public bool Beschaedigungsgrad { get; set; }
}