namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// Represents the data required to reset a user's password.
    /// </summary>
    public class ResetPasswordApiDTO
    {
        /// <summary>
        /// The email address of the user requesting the password reset.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The token received in the reset password email.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The new password the user wants to set.
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}
