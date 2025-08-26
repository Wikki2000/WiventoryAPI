using System.ComponentModel.DataAnnotations;
using WiventoryAPI.Models;

namespace WiventoryAPI.DTOs
{
    public class CreateHotelRequestDto
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Contact { get; set; }
        public required string Email { get; set; }
    }
}
