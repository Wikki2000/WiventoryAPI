using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WiventoryAPI.Models
{
    public class Hotel : BaseModel
    {
        [MaxLength(225)]
        public string? LogoPath { get; set; }

        public byte[]? Logo { get; set; }

        [Required]
        [MaxLength(225)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(225)]
        public required string Location { get; set; }

        [Required]
        [MaxLength(225)]
        public required string Contact { get; set; }

        [MaxLength(225)]
        public string? AltContact { get; set; }

        [Required]
        [MaxLength(225)]
        [EmailAddress]
        public required string Email { get; set; }

        [MaxLength(225)]
        public string? Website { get; set; }

        [Required]
        [MaxLength(225)]
        public required string Subdomain { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsFreeTrial { get; set; } = false;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public float Amount { get; set; } = 20000;

        [Required]
        public DateTime EndDate { get; set; }

        // Navigation properties for relationships
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
