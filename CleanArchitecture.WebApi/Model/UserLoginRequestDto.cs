using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.WebApi.Model
{
    public class UserLoginRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
