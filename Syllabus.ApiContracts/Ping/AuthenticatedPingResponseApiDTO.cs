namespace Syllabus.ApiContracts.Ping
{
    /// <summary>
    /// Response model for authenticated ping operations.
    /// </summary>
    public class AuthenticatedPingResponseApiDTO : PingResponseApiDTO
    {
        /// <summary>
        /// The ID of the authenticated user.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// The email of the authenticated user.
        /// </summary>
        public string? UserEmail { get; set; }
    }
} 