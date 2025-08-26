using System;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

namespace WiventoryAPI.Models
{
    public enum Title
    {
        Mr, Mrs, Ms,
        Rev, Dr, Sir,
        Prof, Lady
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum Role
    {
        Admin,
        Manager,
        Staff
    }

    public class User : BaseModel
    {
        [Required]
        public Title? Title { get; set; }

        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

	[MaxLength(50)]
	public string? MiddleName { get; set; }

	[Required, MaxLength(50)]
	public required string LastName { get; set; }	

        [MaxLength(20)]
        public required string UserName { get; set; }

        public byte[]? ProfilePhoto { get; set; }

        [Required, MaxLength(225), EmailAddress]
        public required string Email { get; set; }

        public Gender? Gender { get; set; }

        public string? Number { get; set; }
        public string? Address { get; set; }

        public float? Salary { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DOB { get; set; }

        public string? NOK { get; set; }
        public string? NOKNumber { get; set; }
        public string? Religion { get; set; }
        public string? State { get; set; }

        [Required, MaxLength(1500)]
        public required string Password { get; set; }

        public required int RankNumber { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public required string Portfolio { get; set; }

	[Required]
	[Range(0, 100, ErrorMessage = "Performance must be between 0 and 100.")]
	public int Performance { get; set; } = 50;

        public bool IsActive { get; set; } = false;
        public DateTime? LastActive { get; set; }
	
	// Foreign key to Hotel
 	[Required]
	public Guid HotelId { get; set; }

	// Navigation property to Hotel.
	public virtual Hotel Hotel { get; set; } = null!;

        /// <summary>
        /// Hashes a plain text password using BCrypt.
        /// </summary>
        /// <param name="password">The plain password to hash.</param>
        /// <returns>The hashed password.</returns>
        public void HashPassword()
        {
            this.Password = BCrypt.Net.BCrypt.HashPassword(this.Password);
        }

        /// <summary>
        /// Verifies if a plain password matches the stored hash.
        /// </summary>
        /// <param name="password">Plain password to check.</param>
        /// <returns>True if the password matches the hash.</returns>
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, this.Password);
        }
    }
}
