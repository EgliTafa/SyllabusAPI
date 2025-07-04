using Syllabus.ApiContracts.Authentication;

namespace SyllabusAPI.RequestExamples.Authentication
{
    /// <summary>
    /// Example request for user login.
    /// </summary>
    public class LoginRequestExample
    {
        /// <summary>
        /// Gets an example login request.
        /// </summary>
        public static LoginRequestApiDTO Example => new()
        {
            Email = "john.doe@example.com",
            Password = "SecurePassword123!"
        };
    }
} 