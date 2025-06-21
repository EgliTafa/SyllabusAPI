using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The request model for revoking user access.
    /// </summary>
    public class RevokeUserAccessRequestApiDTO
    {
        /// <summary>
        /// The reason for revoking access (optional).
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// The duration of the lockout in days (optional, defaults to permanent).
        /// </summary>
        [JsonPropertyName("lockoutDurationDays")]
        public int? LockoutDurationDays { get; set; }
    }
} 