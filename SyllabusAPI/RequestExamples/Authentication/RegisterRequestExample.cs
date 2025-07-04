using Syllabus.ApiContracts.Authentication;

namespace SyllabusAPI.RequestExamples.Authentication
{
    /// <summary>
    /// Example request for user registration.
    /// </summary>
    public class RegisterRequestExample
    {
        /// <summary>
        /// Gets an example registration request.
        /// </summary>
        public static RegisterUserRequestApiDTO Example => new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "SecurePassword123!",
            PhonePrefix = "+355",
            PhoneNumber = "691234567",
            ProfilePictureUrl = "https://example.com/profile.jpg"
        };
    }
} 