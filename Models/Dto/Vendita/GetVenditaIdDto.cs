namespace BuildWeek2.Models.Dto.Vendita;

public class GetVenditaIdDto
{

    public Guid VenditaId { get; set; }
    public DateTime DataVendita { get; set; }
    public string CodiceFiscale { get; set; }
    public string? NumeroRicetta { get; set; }
}
