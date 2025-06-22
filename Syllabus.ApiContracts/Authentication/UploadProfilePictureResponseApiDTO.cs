using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Response DTO for profile picture upload.
    /// </summary>
    public class UploadProfilePictureResponseApiDTO
    {
        /// <summary>
        /// The URL of the uploaded profile picture.
        /// </summary>
        [JsonPropertyName("profilePictureUrl")]
        public string ProfilePictureUrl { get; set; } = string.Empty;

        /// <summary>
        /// Success message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
} 