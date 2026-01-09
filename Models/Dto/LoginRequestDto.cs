using System.ComponentModel.DataAnnotations;

namespace BuildWeek2.Models.Dto
{
    public class LoginRequestDto
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}
