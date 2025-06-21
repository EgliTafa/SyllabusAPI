using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The request model for creating a user by an administrator.
    /// </summary>
    public class CreateUserByAdminRequestApiDTO
    {
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
        /// The password for the new user account.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// The prefix of the phone number (e.g., +355).
        /// </summary>
        [JsonPropertyName("phonePrefix")]
        public string PhonePrefix { get; set; } = string.Empty;

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// The role to assign to the user.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// The URL to the user's profile picture (optional).
        /// </summary>
        [JsonPropertyName("profilePictureUrl")]
        public string? ProfilePictureUrl { get; set; }
    }
} 