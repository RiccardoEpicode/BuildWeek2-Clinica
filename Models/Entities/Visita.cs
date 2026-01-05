using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek2.Models.Entities
{
    public class Visita
    {
        [Key]
        public Guid VisitaId { get; set; }
        [Required]
        public DateTime DataVisita { get; set; }
        [Required]
        public string EsameEffettuato { get; set; }
        public string? DescrizioneEsame { get; set; }

        [Required]
        public Guid AnimaleId { get; set; }
        public Animale Animale { get; set; }

        [Required]
        public Guid? RicoveroAnimaleSmarritoId { get; set; }
        public RicoveroAnimaleSmarrito? RicoveroAnimaleSmarrito { get; set; }
    }
}
