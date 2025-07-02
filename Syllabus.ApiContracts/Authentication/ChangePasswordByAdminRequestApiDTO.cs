using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Request DTO for admin password change.
    /// </summary>
    public class ChangePasswordByAdminRequestApiDTO
    {
        /// <summary>
        /// The new password for the user.
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