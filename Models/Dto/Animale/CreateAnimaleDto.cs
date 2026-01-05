using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto.Animale
{
    public class CreateAnimaleDto
    {
        [Required]
        public DateTime DataRegistrazione { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Tipologia { get; set; }

        public string ColoreMantello { get; set; }

        public DateTime? DataNascita { get; set; }

        public bool PresenzaMicrochip { get; set; }

        public string? NumeroMicrochip { get; set; }

        [Required]
        public string Proprietario { get; set; }
    }
}
