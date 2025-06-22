using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Request DTO for uploading a profile picture.
    /// </summary>
    public class UploadProfilePictureRequestApiDTO
    {
        /// <summary>
        /// The profile picture file as a base64 string.
        /// </summary>
        [JsonPropertyName("file")]
        public string File { get; set; } = string.Empty;

        /// <summary>
        /// The file name of the uploaded image.
        /// </summary>
        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// The content type of the file (e.g., image/jpeg, image/png).
        /// </summary>
        [JsonPropertyName("contentType")]
        public string ContentType { get; set; } = string.Empty;
    }
} 