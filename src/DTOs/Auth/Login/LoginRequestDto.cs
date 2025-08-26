using System.ComponentModel.DataAnnotations;

namespace WiventoryAPI.DTOs
{
    public class LoginRequestDto
    {
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, MinLength(8)]
        public required string Password { get; set; }
    }
}

