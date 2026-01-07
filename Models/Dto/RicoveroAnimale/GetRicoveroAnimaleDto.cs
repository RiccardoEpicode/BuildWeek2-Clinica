namespace BuildWeek2.Models.Dto.RicoveroAnimale;

public class GetRicoveroAnimaleDto
{
<<<<<<< HEAD
    public Guid RicoveroAnimaleId { get; set; }
    public DateTime DataInizioRicovero { get; set; }
    public DateTime? DataFineRicovero { get; set; }
    public bool Attivo { get; set; }
     public string NomeAnimale { get; set; }
=======
    public class GetRicoveroAnimaleDto
    {
        public Guid RicoveroAnimaleId { get; set; }
        public DateTime DataInizioRicovero { get; set; }
        public DateTime? DataFineRicovero { get; set; }
        public bool Attivo { get; set; }

        public Guid AnimaleId { get; set; }
        public string NomeAnimale { get; set; }
    }
>>>>>>> 8bc3e0e013868a840b55cca4051ce7314a36fd45
}
