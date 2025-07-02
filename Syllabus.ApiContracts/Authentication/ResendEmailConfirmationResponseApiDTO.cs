using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Response DTO for resending email confirmation.
    /// </summary>
    public class ResendEmailConfirmationResponseApiDTO
    {
        /// <summary>
        /// Gets or sets the success message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
    }
} 