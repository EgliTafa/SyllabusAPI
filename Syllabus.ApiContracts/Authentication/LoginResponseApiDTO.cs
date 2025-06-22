using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The response model returned after a successful login.
    /// </summary>
    public class LoginResponseApiDTO
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        /// <summary>
        /// The first name of the user.
        /// </summary>
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = default!;

        /// <summary>
        /// The last name of the user.
        /// </summary>
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = default!;

        /// <summary>
        /// The email address of the user.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;

        /// <summary>
        /// The JWT token issued upon successful authentication.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = default!;

        /// <summary>
        /// Whether the user's email is confirmed.
        /// </summary>
        [JsonPropertyName("emailConfirmed")]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// The URL of the user's profile picture.
        /// </summary>
        [JsonPropertyName("profilePictureUrl")]
        public string? ProfilePictureUrl { get; set; }

        /// <summary>
        /// Whether the user account is locked out.
        /// </summary>
        [JsonPropertyName("lockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// The date and time when the lockout ends (null if not locked out).
        /// </summary>
        [JsonPropertyName("lockoutEnd")]
        public string? LockoutEnd { get; set; }

        /// <summary>
        /// The current status of the user account.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = default!;

        /// <summary>
        /// The reason for the user's lockout (if applicable).
        /// </summary>
        [JsonPropertyName("lockoutReason")]
        public string? LockoutReason { get; set; }
    }
}
