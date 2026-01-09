using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Data
{
    public class ApplicationUser :IdentityUser
    {
       [Required(ErrorMessage ="Nome e Cognome sono obbligatori!")]
       [MaxLength(100)]
       public string NomeCompleto { get; set; }
       [Required]
       public DateTime DataNascita { get; set; }
       [Required]
       [MaxLength(16)]
       public string CodiceFiscale { get; set; }

    }
}
