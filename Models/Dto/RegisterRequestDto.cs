using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NomeCompleto { get; set; }

    }
}
