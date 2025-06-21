namespace Syllabus.ApiContracts.Ping
{
    /// <summary>
    /// Response model for ping operations.
    /// </summary>
    public class PingResponseApiDTO
    {
        /// <summary>
        /// The message indicating the API status.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp when the ping was made.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The API version.
        /// </summary>
        public string Version { get; set; } = string.Empty;
    }
} 