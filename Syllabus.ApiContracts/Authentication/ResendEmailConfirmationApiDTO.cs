using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Request DTO for resending email confirmation.
    /// </summary>
    public class ResendEmailConfirmationApiDTO
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting email confirmation resend.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;
    }
} 