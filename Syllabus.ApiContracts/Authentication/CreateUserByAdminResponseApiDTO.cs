using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The response model for creating a user by an administrator.
    /// </summary>
    public class CreateUserByAdminResponseApiDTO
    {
        /// <summary>
        /// The unique identifier of the created user.
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
        /// The role assigned to the user.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// The status of the user account.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
} 