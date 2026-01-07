namespace BuildWeek2.Models.Dto.Vendita;

public class CreateVenditaDto
{
    public DateTime DataVendita { get; set; }
    public string CodiceFiscale { get; set; }
    public string? NumeroRicetta { get; set; }
  public int ProdottiId { get; set; }
    public Guid FarmacistaId { get; set; }
}
