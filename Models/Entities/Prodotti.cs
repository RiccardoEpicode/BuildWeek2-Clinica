using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildWeek2.Models.Entities
{
    public class Prodotti
    {
        [Key]
        public Guid ProdottiId { get; set; }
        [Required]
        public string NomeProdotto { get; set; }
        public bool Medicinale { get; set; }
        [Required]
        public string Usi { get; set; }
        public int CodiceArmadietto { get; set; }
        public int CodiceCassetto { get; set; }

        [Required]
        public string FornitoreId { get; set; }
        public Fornitore Fornitore { get; set; }
        public ICollection<Vendita> Vendite { get; set; } = new List<Vendita>();
    }
}
