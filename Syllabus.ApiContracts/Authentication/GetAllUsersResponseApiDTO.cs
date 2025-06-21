using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The response model for getting all users.
    /// </summary>
    public class GetAllUsersResponseApiDTO
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The first name of the user.
        /// </summary>
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the user.
        /// </summary>
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the user.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Whether the user's email is confirmed.
        /// </summary>
        [JsonPropertyName("emailConfirmed")]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Whether the user account is locked out.
        /// </summary>
        [JsonPropertyName("lockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// The lockout end date (if locked out).
        /// </summary>
        [JsonPropertyName("lockoutEnd")]
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// The status of the user account.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// The roles assigned to the user.
        /// </summary>
        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; } = new List<string>();
    }
} 