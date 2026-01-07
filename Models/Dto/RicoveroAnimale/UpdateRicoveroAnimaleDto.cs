namespace BuildWeek2.Models.Dto.RicoveroAnimale;

public class UpdateRicoveroAnimaleDto
{
    public DateTime DataInizioRicovero { get; set; }
    public DateTime? DataFineRicovero { get; set; }
    public bool Attivo { get; set; } = false;

    public string NomeAnimale { get; set; }
}
