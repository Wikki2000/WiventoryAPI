using System.ComponentModel.DataAnnotations;
using WiventoryAPI.Models;

namespace WiventoryAPI.DTOs
{
    public class CreateUserRequestDto
    {
        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(20)]
        public string? UserName { get; set; }

        [Required, MaxLength(225), EmailAddress]
        public required string Email { get; set; }  // <-- added string type

        [Required, MaxLength(1500)]
        public required string Password { get; set; }

        public required Role Role { get; set; }

        public required Guid HotelId { get; set; }
    }
}

