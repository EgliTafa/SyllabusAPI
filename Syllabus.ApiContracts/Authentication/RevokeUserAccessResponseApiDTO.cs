using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The response model for revoking user access.
    /// </summary>
    public class RevokeUserAccessResponseApiDTO
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the user.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Whether the user account is now locked out.
        /// </summary>
        [JsonPropertyName("lockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// The lockout end date.
        /// </summary>
        [JsonPropertyName("lockoutEnd")]
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// The status of the user account.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// The message indicating the result of the operation.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
} 