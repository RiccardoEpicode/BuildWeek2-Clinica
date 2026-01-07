namespace BuildWeek2.Models.Dto.RicoveroAnimale
{
    public class GetRicoveroAnimaleDto
    {
        public Guid RicoveroAnimaleId { get; set; }
        public DateTime DataInizioRicovero { get; set; }
        public DateTime? DataFineRicovero { get; set; }
        public bool Attivo { get; set; }

        public Guid AnimaleId { get; set; }
        public string NomeAnimale { get; set; }
    }
}
