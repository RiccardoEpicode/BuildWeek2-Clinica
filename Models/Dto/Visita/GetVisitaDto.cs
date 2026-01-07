namespace BuildWeek2.Models.Dto.Visita
{
    public class GetVisitaDto
    {
        public Guid VisitaId { get; set; }
        public DateTime DataVisita { get; set; }
        public string EsameEffettuato { get; set; }
        public string? DescrizioneEsame { get; set; }
    }
}
