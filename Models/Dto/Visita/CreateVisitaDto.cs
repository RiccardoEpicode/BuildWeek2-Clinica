namespace BuildWeek2.Models.Dto.Visita
{
    public class CreateVisitaDto
    {
        public Guid VisitaId { get; set; }
        public DateTime DataVisita { get; set; }
        public string EsameEffettuato { get; set; }
        public string? DescrizioneEsame { get; set; }
        public Guid AnimaleId { get; set; }
        public Guid? RicoveroAnimaleSmarritoId { get; set; }
    }
}
