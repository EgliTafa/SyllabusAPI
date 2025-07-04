namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Represents the response message after attempting a password reset.
    /// </summary>
    public class ResetPasswordResponseApiDTO
    {
        /// <summary>
        /// The result message indicating whether the password reset was successful.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
