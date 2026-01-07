using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto.Animale
{
    public class GetAnimaleIdDto
    {
        public Guid AnimaleId { get; set; }

        public string Nome { get; set; }

        public string Tipologia { get; set; }

        public string Proprietario { get; set; }

        public string? NumeroMicrochip { get; set; }
    }
}
