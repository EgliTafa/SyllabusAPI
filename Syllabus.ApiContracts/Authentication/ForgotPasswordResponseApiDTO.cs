namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Response DTO after requesting a password reset.
    /// </summary>
    public class ForgotPasswordResponseApiDTO
    {
        /// <summary>
        /// Gets or sets the informational message regarding the password reset request.
        /// </summary>
        public string Message { get; set; } = default!;
    }
}
