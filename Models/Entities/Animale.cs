using BuildWeek2.Models.Entities;
using System.ComponentModel.DataAnnotations;

public class Animale
{
    [Key] 
    public Guid AnimaleId { get; set; }

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

    public ICollection<Visita> Visite { get; set; } = new List<Visita>();
}
