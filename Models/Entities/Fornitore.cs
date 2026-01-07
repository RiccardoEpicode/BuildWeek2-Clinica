using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Entities;

public class Fornitore
{
    [Key]
    public Guid FornitoreId { get; set; }
    [Required]
    public string Nome{ get; set; }
    public string? Recapito { get; set; }
    public string? Indirizzo { get; set; }
    public ICollection<Prodotti> Prodotti { get; set; } = new List<Prodotti>();

}
