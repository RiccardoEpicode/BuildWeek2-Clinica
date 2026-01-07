using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto.Fornitore
{
    public class UpdateFornitoreDto
    {
        public string Nome { get; set; }
        public string? Recapito { get; set; }
        public string? Indirizzo { get; set; }
    }
}
