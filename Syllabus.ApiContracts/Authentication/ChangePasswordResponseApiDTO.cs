namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Represents the response after a password change operation.
    /// </summary>
    public class ChangePasswordResponseApiDTO
    {
        /// <summary>
        /// The result message indicating whether the password change was successful.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp when the password was changed.
        /// </summary>
        public DateTime ChangedAt { get; set; }
    }
} 