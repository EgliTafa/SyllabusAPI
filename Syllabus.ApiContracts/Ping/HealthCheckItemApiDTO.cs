namespace Syllabus.ApiContracts.Ping
{
    /// <summary>
    /// Represents an individual health check item.
    /// </summary>
    public class HealthCheckItemApiDTO
    {
        /// <summary>
        /// The name of the health check component.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The status of the health check (Healthy, Warning, Unhealthy).
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// The response time in milliseconds.
        /// </summary>
        public long ResponseTime { get; set; }

        /// <summary>
        /// Additional message or details about the health check.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
} 