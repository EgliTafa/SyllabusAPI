namespace Syllabus.ApiContracts.Authentication
{
    /// <summary>
    /// The response model returned after a successful user registration.
    /// </summary>
    public class RegisterUserResponseApiDTO
    {
        /// <summary>
        /// The unique identifier of the newly registered user.
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
        /// The JWT token issued upon successful registration.
        /// </summary>
        public string Token { get; set; } = default!;
    }
}
