using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Represents the data required to change a user's password.
    /// </summary>
    public class ChangePasswordRequestApiDTO
    {
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// The user's current password.
        /// </summary>
        [JsonPropertyName("currentPassword")]
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// The new password the user wants to set.
        /// </summary>
        [JsonPropertyName("newPassword")]
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Confirmation of the new password.
        /// </summary>
        [JsonPropertyName("confirmPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
} 