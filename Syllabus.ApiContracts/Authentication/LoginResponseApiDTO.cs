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
    }
}
