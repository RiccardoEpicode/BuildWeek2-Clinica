using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NomeCompleto { get; set; }
        public string CodiceFiscale { get; set; }
    }

}
