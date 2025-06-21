using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The request model for user login.
    /// </summary>
    public class LoginRequestApiDTO
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;

        /// <summary>
        /// The user's password.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; } = default!;
    }
}
