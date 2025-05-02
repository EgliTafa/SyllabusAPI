namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The request model for user login.
    /// </summary>
    public class LoginRequestApiDTO
    {
        /// <summary>
        /// The email address of the user attempting to log in.
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// The password of the user attempting to log in.
        /// </summary>
        public string Password { get; set; } = default!;
    }
}
