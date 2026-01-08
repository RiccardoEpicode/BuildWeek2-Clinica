using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
