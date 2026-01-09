using BuildWeek2.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek2.Models.Entities;

public class Vendita
{
    [Key]
    public Guid VenditaId { get; set; }
    [Required]
    public DateTime DataVendita { get; set; }
    [Required]
    [MaxLength(16)]
    public string CodiceFiscale { get; set; }
    public string? NumeroRicetta { get; set; }

    [Required]
    public Guid ProdottiId { get; set; }
    public Prodotti Prodotti { get; set; }

    [Required]
    public string FarmacistaId { get; set; }
    public ApplicationUser Farmacista { get; set; }
}
