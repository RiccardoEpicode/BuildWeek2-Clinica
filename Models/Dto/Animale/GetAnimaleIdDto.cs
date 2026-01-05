using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto.Animale
{
    public class GetAnimaleIdDto
    {
        public Guid AnimaleId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Tipologia { get; set; }

        [Required]
        public string Proprietario { get; set; }

        [Required]
        public string? NumeroMicrochip { get; set; }
    }
}
