namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The response model returned after a successful login.
    /// </summary>
    public class LoginResponseApiDTO
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public string Id { get; set; } = default!;

        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; set; } = default!;

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; set; } = default!;

        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// The JWT token issued upon successful authentication.
        /// </summary>
        public string Token { get; set; } = default!;
    }
}
