using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto.RicoveroAnimaleSmarrito
{
    public class UpdateAnimaleSmarritoDto
    {
                
        public DateTime DataInizioRicoveroSmarrito { get; set; }
        public DateTime? DataFineRicoveroSmarrito { get; set; }
        public string Tipologia { get; set; }
        public string ColoreMantello { get; set; }
        public DateTime? DataNascita { get; set; }
        public string? NumeroMicrochip { get; set; }
        public bool Attivo { get; set; }
    }
}
