using System;
using System.Threading.Tasks;
using System.Linq.Expressions;

using WiventoryAPI.Data;
using WiventoryAPI.Models;
using WiventoryAPI.DTOs;
using WiventoryAPI.Errors;


namespace WiventoryAPI.Services
{
    /// <summary>
    /// Interface for authentication-related services.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Logs in a user using email and password.
        /// </summary>
        /// <param name="dto">Login request DTO.</param>
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }

    /// <summary>
    /// Service implementation for authentication-related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
	private readonly IStorageService<User> _storage;

        public AuthService(IStorageService<User> storage )
        {
            _storage = storage;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
        /*    if (dto == null)
                throw new HttpError(400, "Request body cannot be empty.");

            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                throw new HttpError(400, "Email and password are required.");

            // Fetch user from storage by email
	    Expression<Func<User, bool>> emailPredicate = u => u.Email == "wisdomokposin@gmail.com";
            var user = await _storage.GetByAsync<User>(emailPredicate);
            if (user == null)
                throw new HttpError(404, "User not found.");

            // Validate password (example, replace with real hash check)
            if (user.Password != dto.Password)
                throw new HttpError(401, "Invalid credentials.");
		*/

            // Generate a JWT token or dummy token
            var token = Guid.NewGuid().ToString(); // Replace with JWT logic

            // Return response DTO
            return new LoginResponseDto
            {
                Token = token,
                UserName = "wikki",
                Email = "wikkiy",
		Performance = 50 
            };
        }
    }
}
