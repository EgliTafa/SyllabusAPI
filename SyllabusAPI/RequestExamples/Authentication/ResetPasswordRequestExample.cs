using Syllabus.ApiContracts.Authentication;

namespace SyllabusAPI.RequestExamples.Authentication
{
    /// <summary>
    /// Example request for reset password.
    /// </summary>
    public class ResetPasswordRequestExample
    {
        /// <summary>
        /// Gets an example reset password request.
        /// </summary>
        public static ResetPasswordApiDTO Example => new()
        {
            Email = "john.doe@example.com",
            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...", // Example JWT token
            NewPassword = "NewSecurePassword123!"
        };
    }
} 