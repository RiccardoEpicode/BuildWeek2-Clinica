namespace BuildWeek2.Models.Dto.Prodotti
{
    public class UpdateProdottiDto
    {
        public string NomeProdotto { get; set; }
        public bool Medicinale { get; set; }
        public string Usi { get; set; }
        public int CodiceArmadietto { get; set; }
        public int CodiceCassetto { get; set; }
    }
}
