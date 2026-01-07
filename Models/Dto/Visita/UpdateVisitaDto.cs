using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto.Visita
{
    public class UpdateVisitaDto
    {
        public DateTime DataVisita { get; set; }
        public string EsameEffettuato { get; set; }
        public string? DescrizioneEsame { get; set; }
    }
}
