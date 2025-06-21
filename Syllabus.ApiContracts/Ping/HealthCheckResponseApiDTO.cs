using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Ping
{
    /// <summary>
    /// Response model for comprehensive health check operations.
    /// </summary>
    public class HealthCheckResponseApiDTO
    {
        /// <summary>
        /// The overall status of the system (Healthy, Warning, Unhealthy).
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp when the health check was performed.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The API version.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// List of individual health check results.
        /// </summary>
        [JsonPropertyName("checks")]
        public List<HealthCheckItemApiDTO> Checks { get; set; } = new List<HealthCheckItemApiDTO>();
    }
} 