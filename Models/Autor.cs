using System.ComponentModel.DataAnnotations;

namespace Hochwaerts.Models;

public class Autor
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    // Foreign Key
    public int BuchId { get; set; }

    // Navigation Property
    public Buch Buch { get; set; }
}