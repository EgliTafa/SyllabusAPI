namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Request DTO for initiating a forgot password flow.
    /// </summary>
    public class ForgotPasswordApiDTO
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting password reset.
        /// </summary>
        public string Email { get; set; } = default!;
    }
}
