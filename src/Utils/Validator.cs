using System;
using System.Collections.Generic;
using System.Linq;

namespace WiventoryAPI.Utils
{
    /// <summary>
    /// Provides utility methods for validating request data and common inputs like passwords.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Checks a request body for missing or empty required fields.
        /// </summary>
        /// <param name="data">The request data as a dictionary.</param>
        /// <param name="requiredFields">A list of required field names.</param>
        /// <returns>
        /// A dictionary containing an "error" key if validation fails, or null if the data is valid.
        /// </returns>
        public static Dictionary<string, string>? BadRequest(Dictionary<string, object> data, List<string>? requiredFields = null)
        {
            if (data == null || data.Count == 0)
                return new Dictionary<string, string> { { "error", "Empty Request Body" } };

            if (requiredFields != null)
            {
                foreach (var field in requiredFields)
                {
                    if (!data.ContainsKey(field) || data[field] == null)
                        return new Dictionary<string, string> { { "error", $"{field} is required" } };
                }
            }

            return null;
        }

        /// <summary>
        /// Validates the strength of a password.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the password meets all strength criteria; otherwise, false.</returns>
        /// <remarks>
        /// Criteria:
        /// - Minimum 8 characters
        /// - At least one uppercase letter
        /// - At least one lowercase letter
        /// - At least one digit
        /// - At least one special character
        /// </remarks>
        public static bool ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        /// <summary>
        /// Validates the format of an email address.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <returns>True if the email is in a valid format; otherwise, false.</returns>
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
