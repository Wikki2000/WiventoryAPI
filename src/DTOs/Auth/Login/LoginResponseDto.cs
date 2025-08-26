using System.ComponentModel.DataAnnotations;

namespace WiventoryAPI.DTOs
{
    public class LoginResponseDto
    {
        public required string UserName { get; set; }

	[Required, EmailAddress]
	public required string Email { get; set; }

        public required string Token { get; set; }
        public required int Performance { get; set; }
    }
}

