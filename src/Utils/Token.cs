using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WiventoryAPI.Models;

namespace WiventoryAPI.Utils
{
    public static class TokenUtils
    {
        // Read secret from environment variables
        private static readonly string JwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") 
            ?? throw new Exception("JWT_SECRET is not defined in environment variables");

        /// <summary>
        /// Generate a numeric token string with specified length.
        /// </summary>
        public static string GenerateNumericToken(int length = 6)
        {
            var random = new Random();
            var token = "";
            for (int i = 0; i < length; i++)
            {
                token += random.Next(0, 10);
            }
            return token;
        }

        /// <summary>
        /// Generate a JWT token string with optional expiration.
        /// </summary>
        /// <param name="user">User object containing Id, UserName, Email, Role</param>
        /// <param name="expiryMinutes">Expiration in minutes (optional)</param>
        /// <returns>JWT token string</returns>
        public static string GenerateJwtToken(User user, int? expiryMinutes = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString() ?? "User") // fixed
            };

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: expiryMinutes.HasValue ? DateTime.UtcNow.AddMinutes(expiryMinutes.Value) : null,
                    signingCredentials: creds
                    );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Verify a JWT token and return the payload as a string (JSON).
        /// </summary>
        public static string? VerifyJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(JwtSecret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                        {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                        }, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken == null) return null;

                // Return payload as JSON string
                return jwtToken.Claims.FirstOrDefault(c => c.Type == "data")?.Value;
            }
            catch
            {
                // Invalid token
                return null;
            }
        }
    }
}
