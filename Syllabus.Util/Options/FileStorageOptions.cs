namespace Syllabus.Util.Options
{
    /// <summary>
    /// Configuration options for file storage.
    /// </summary>
    public class FileStorageOptions
    {
        public const string SectionName = "FileStorage";
        
        /// <summary>
        /// The base path where files will be stored.
        /// </summary>
        public string UploadPath { get; set; } = "wwwroot/uploads";
        
        /// <summary>
        /// The base URL for accessing uploaded files.
        /// This can be overridden via environment variable FileStorage__BaseUrl
        /// </summary>
        public string BaseUrl { get; set; } = "http://localhost:5000";
        
        /// <summary>
        /// Maximum file size in bytes (default: 5MB).
        /// </summary>
        public long MaxFileSize { get; set; } = 5 * 1024 * 1024;
        
        /// <summary>
        /// Allowed file extensions for profile pictures.
        /// </summary>
        public string[] AllowedExtensions { get; set; } = { ".jpg", ".jpeg", ".png", ".gif" };
    }
} 