using Syllabus.ApiContracts.Authentication;

namespace SyllabusAPI.RequestExamples.Authentication
{
    /// <summary>
    /// Example request for forgot password.
    /// </summary>
    public class ForgotPasswordRequestExample
    {
        /// <summary>
        /// Gets an example forgot password request.
        /// </summary>
        public static ForgotPasswordApiDTO Example => new()
        {
            Email = "john.doe@example.com"
        };
    }
} 