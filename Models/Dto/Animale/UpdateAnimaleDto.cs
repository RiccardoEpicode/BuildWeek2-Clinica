namespace BuildWeek2.Models.Dto.Animale
{
    public class UpdateAnimaleDto : CreateAnimaleDto
    {
        public DateTime DataRegistrazione { get; set; }
        public string Nome { get; set; }
        public string Tipologia { get; set; }
        public string ColoreMantello { get; set; }
        public DateTime DataNascita { get; set; }
        public bool PresenzaMicrochip { get; set; }
        public string? NumeroMicrochip { get; set; }
        public string Proprietario { get; set; }



    }
}
