using System.ComponentModel.DataAnnotations;


namespace BuildWeek2.Models.Entities;

<<<<<<< HEAD
public class RicoveroAnimale 
{
    [Key]
    public Guid RicoveroAnimaleId { get; set; }
    [Required]
    public DateTime DataInizioRicovero { get; set; }
    public DateTime? DataFineRicovero { get; set; }
    public bool Attivo { get; set; }

    [Required]
    public Guid AnimaleId { get; set; }
    public Animale Animale { get; set; }    
=======
        [Required]
        public Guid AnimaleId { get; set; }
        public Animale Animale { get; set; }
    }
>>>>>>> 8bc3e0e013868a840b55cca4051ce7314a36fd45
}
