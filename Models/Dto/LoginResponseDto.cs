using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        
        public DateTime Expiration { get; set; }
    }
}
