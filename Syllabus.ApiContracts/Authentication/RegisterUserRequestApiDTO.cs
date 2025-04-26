namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The request model for user registration.
    /// </summary>
    public class RegisterUserRequestApiDTO
    {
        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The password for the new user account.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// The prefix of the phone number (e.g., +355).
        /// </summary>
        public string PhonePrefix { get; set; } = string.Empty;

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// The URL to the user's profile picture (optional).
        /// </summary>
        public string? ProfilePictureUrl { get; set; }
    }
}
