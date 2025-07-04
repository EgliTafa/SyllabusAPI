using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Response DTO for admin password change.
    /// </summary>
    public class ChangePasswordByAdminResponseApiDTO
    {
        /// <summary>
        /// Success message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp when the password was changed.
        /// </summary>
        [JsonPropertyName("changedAt")]
        public string ChangedAt { get; set; } = string.Empty;
    }
} 