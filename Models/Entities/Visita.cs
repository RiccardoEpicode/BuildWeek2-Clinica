using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek2.Models.Entities;

public class Visita
{
    [Key]
    public Guid VisitaId { get; set; }
    [Required]
    public DateTime DataVisita { get; set; }
    [Required]
    public string EsameEffettuato { get; set; }
    public string? DescrizioneEsame { get; set; }

    
    public Guid? AnimaleId { get; set; }
    public ICollection<Animale> Animali { get; set; } = new List<Animale>();

    public Guid? RicoveroAnimaleSmarritoId { get; set; }
    public ICollection<RicoveroAnimaleSmarrito> RicoveroAnimaleSmarriti { get; set; } = new List<RicoveroAnimaleSmarrito>();
}
